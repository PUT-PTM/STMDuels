
#define HSE_VALUE ((uint32_t)8000000) /* STM32 discovery uses a 8Mhz external crystal */

#include "stm32f4xx_conf.h"
#include "stm32f4xx.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_exti.h"
#include "stm32f4xx_spi.h"

#include "usbd_cdc_core.h"
#include "usbd_usr.h"
#include "usbd_desc.h"
#include "usbd_cdc_vcp.h"
#include "usb_dcd_int.h"

#include "stm32f4_discovery_lis302dl.h"
#include "misc.h"
#include "protocol.h"
#include "stdlib.h"
#include "math.h"

volatile uint32_t ticker, downTicker;

int acc1, acc2, acc3;

/*
 * The USB data must be 4 byte aligned if DMA is enabled. This macro handles
 * the alignment, if necessary (it's actually magic, but don't tell anyone).
 */
__ALIGN_BEGIN USB_OTG_CORE_HANDLE  USB_OTG_dev __ALIGN_END;


void init();
void ColorfulRingOfDeath(void);

/*
 * Define prototypes for interrupt handlers here. The conditional "extern"
 * ensures the weak declarations from startup_stm32f4xx.c are overridden.
 */
#ifdef __cplusplus
 extern "C" {
#endif

void SysTick_Handler(void);
void NMI_Handler(void);
void HardFault_Handler(void);
void MemManage_Handler(void);
void BusFault_Handler(void);
void UsageFault_Handler(void);
void SVC_Handler(void);
void DebugMon_Handler(void);
void PendSV_Handler(void);
void OTG_FS_IRQHandler(void);
void OTG_FS_WKUP_IRQHandler(void);

#ifdef __cplusplus
}
#endif

 setup_przycisk() {
 	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA, ENABLE);
 	GPIO_InitTypeDef  Przycisk;
 	Przycisk.GPIO_Pin = GPIO_Pin_0;
 	Przycisk.GPIO_Mode = GPIO_Mode_IN;
 	GPIO_Init(GPIOA, &Przycisk);
 }

 int16_t Round(float myfloat)
 {
   double integral;
   float fraction = (float)modf(myfloat, &integral);

   if (fraction >= 0.5)
     integral += 1;
   if (fraction <= -0.5)
     integral -= 1;

   return (int)integral;
 }


int main(void)
{
	/* Set up the system clocks */
	SystemInit();

	/* Initialize USB, IO, SysTick, and all those other things you do in the morning */
	init();

	SystemCoreClockUpdate();


		    /* Enable the SPI periph */
		    RCC_APB2PeriphClockCmd(LIS302DL_SPI_CLK, ENABLE);

		    /* Enable SCK, MOSI and MISO GPIO clocks */
		    RCC_AHB1PeriphClockCmd(LIS302DL_SPI_SCK_GPIO_CLK | LIS302DL_SPI_MISO_GPIO_CLK | LIS302DL_SPI_MOSI_GPIO_CLK, ENABLE);

		    /* Enable CS  GPIO clock */
		    RCC_AHB1PeriphClockCmd(LIS302DL_SPI_CS_GPIO_CLK, ENABLE);

		    /* Enable INT1 GPIO clock */
		    RCC_AHB1PeriphClockCmd(LIS302DL_SPI_INT1_GPIO_CLK, ENABLE);

		    /* Enable INT2 GPIO clock */
		    RCC_AHB1PeriphClockCmd(LIS302DL_SPI_INT2_GPIO_CLK, ENABLE);

		    GPIO_PinAFConfig(LIS302DL_SPI_SCK_GPIO_PORT, LIS302DL_SPI_SCK_SOURCE, LIS302DL_SPI_SCK_AF);
		    GPIO_PinAFConfig(LIS302DL_SPI_MISO_GPIO_PORT, LIS302DL_SPI_MISO_SOURCE, LIS302DL_SPI_MISO_AF);
		    GPIO_PinAFConfig(LIS302DL_SPI_MOSI_GPIO_PORT, LIS302DL_SPI_MOSI_SOURCE, LIS302DL_SPI_MOSI_AF);

		    GPIO_InitTypeDef GPIO_InitStructure;
		    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
		    GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
		    GPIO_InitStructure.GPIO_PuPd  = GPIO_PuPd_DOWN;
		    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;

		    /* SPI SCK pin configuration */
		    GPIO_InitStructure.GPIO_Pin = LIS302DL_SPI_SCK_PIN;
		    GPIO_Init(LIS302DL_SPI_SCK_GPIO_PORT, &GPIO_InitStructure);

		    /* SPI  MOSI pin configuration */
		    GPIO_InitStructure.GPIO_Pin =  LIS302DL_SPI_MOSI_PIN;
		    GPIO_Init(LIS302DL_SPI_MOSI_GPIO_PORT, &GPIO_InitStructure);

		    /* SPI MISO pin configuration */
		    GPIO_InitStructure.GPIO_Pin = LIS302DL_SPI_MISO_PIN;
		    GPIO_Init(LIS302DL_SPI_MISO_GPIO_PORT, &GPIO_InitStructure);

		    /* SPI configuration -------------------------------------------------------*/
		    SPI_InitTypeDef  SPI_InitStructure;
		    SPI_I2S_DeInit(LIS302DL_SPI);
		    SPI_InitStructure.SPI_Direction = SPI_Direction_2Lines_FullDuplex;
		    SPI_InitStructure.SPI_DataSize = SPI_DataSize_8b;
		    SPI_InitStructure.SPI_CPOL = SPI_CPOL_Low;
		    SPI_InitStructure.SPI_CPHA = SPI_CPHA_1Edge;
		    SPI_InitStructure.SPI_NSS = SPI_NSS_Soft;
		    SPI_InitStructure.SPI_BaudRatePrescaler = SPI_BaudRatePrescaler_4;
		    SPI_InitStructure.SPI_FirstBit = SPI_FirstBit_MSB;
		    SPI_InitStructure.SPI_CRCPolynomial = 7;
		    SPI_InitStructure.SPI_Mode = SPI_Mode_Master;
		    SPI_Init(LIS302DL_SPI, &SPI_InitStructure);

		    /* Enable SPI1  */
		    SPI_Cmd(LIS302DL_SPI, ENABLE);

		    /* Configure GPIO PIN for Lis Chip select */
		    GPIO_InitStructure.GPIO_Pin = LIS302DL_SPI_CS_PIN;
		    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
		    GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
		    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
		    GPIO_Init(LIS302DL_SPI_CS_GPIO_PORT, &GPIO_InitStructure);

		    /* Deselect : Chip Select high */
		    GPIO_SetBits(LIS302DL_SPI_CS_GPIO_PORT, LIS302DL_SPI_CS_PIN);

		    /* Configure GPIO PINs to detect Interrupts */
		    GPIO_InitStructure.GPIO_Pin = LIS302DL_SPI_INT1_PIN;
		    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN;
		    GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
		    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
		    GPIO_InitStructure.GPIO_PuPd  = GPIO_PuPd_NOPULL;
		    GPIO_Init(LIS302DL_SPI_INT1_GPIO_PORT, &GPIO_InitStructure);

		    GPIO_InitStructure.GPIO_Pin = LIS302DL_SPI_INT2_PIN;
		    GPIO_Init(LIS302DL_SPI_INT2_GPIO_PORT, &GPIO_InitStructure);

		    LIS302DL_InterruptConfigTypeDef LIS302DL_InterruptStruct;
		    /* Set configuration of Internal High Pass Filter of LIS302DL*/
		    LIS302DL_InterruptStruct.Latch_Request = LIS302DL_INTERRUPTREQUEST_LATCHED;
		    LIS302DL_InterruptStruct.SingleClick_Axes = LIS302DL_CLICKINTERRUPT_Z_ENABLE;
		    LIS302DL_InterruptStruct.DoubleClick_Axes = LIS302DL_DOUBLECLICKINTERRUPT_Z_ENABLE;
		    LIS302DL_InterruptConfig(&LIS302DL_InterruptStruct);

		    LIS302DL_InitTypeDef  LIS302DL_InitStruct;
	   	/* Set configuration of LIS302DL*/
	   	LIS302DL_InitStruct.Power_Mode = LIS302DL_LOWPOWERMODE_ACTIVE;
	   	LIS302DL_InitStruct.Output_DataRate = LIS302DL_DATARATE_100;
	   	LIS302DL_InitStruct.Axes_Enable = LIS302DL_X_ENABLE | LIS302DL_Y_ENABLE | LIS302DL_Z_ENABLE;
	   	LIS302DL_InitStruct.Full_Scale = LIS302DL_FULLSCALE_2_3;
	   	LIS302DL_InitStruct.Self_Test = LIS302DL_SELFTEST_NORMAL;
	   	LIS302DL_Init(&LIS302DL_InitStruct);

	   	int8_t accelerometer_x, accelerometer_y, accelerometer_z;
	   	int i;

	   	/*inicjalizacja pakietu akcelerometru*/
	   	accelerometer_t accelerometerPacket;
	   	accelerometerPacket.start_flag=0xAA; //0xAA
	   	accelerometerPacket.command=0xAC; //0xAC
	   	accelerometerPacket.crc=0xFF;

	   	/*inicjalizacja pakietu przycisku*/
	   	button_t buttonPacket;
	   	buttonPacket.start_flag=0xAA;
	   	buttonPacket.command=0x38; //0x38
	   	buttonPacket.crc=0xFF;

	   	setup_przycisk();

	while (1)
	{

		LIS302DL_Read(&accelerometer_x, LIS302DL_OUT_X_ADDR, 1);
		if(accelerometer_x>127)
			{
			accelerometer_x=accelerometer_x-1;
			accelerometer_x=(~accelerometer_x)&0xFF;
			accelerometer_x=-accelerometer_x;
			}

		LIS302DL_Read(&accelerometer_y, LIS302DL_OUT_Y_ADDR, 1);
		if(accelerometer_y>127)
		    {
			accelerometer_y=accelerometer_y-1;
			accelerometer_y=(~accelerometer_y)&0xFF;
			accelerometer_y=-accelerometer_y;
		    }

		LIS302DL_Read(&accelerometer_z, LIS302DL_OUT_Z_ADDR, 1);
		if(accelerometer_z>127)
		    {
			accelerometer_z=accelerometer_z-1;
			accelerometer_z=(~accelerometer_z)&0xFF;
			accelerometer_z=-accelerometer_z;
		    }

		for(i=0;i<1000000;i++){

			//petla opozniajaca

		        }

		accelerometerPacket.x=Round(accelerometer_x*0.83);
		accelerometerPacket.y=Round(accelerometer_y*0.83);
		accelerometerPacket.z=Round(accelerometer_z*0.95);


		acc1=accelerometerPacket.x;
		acc2=accelerometerPacket.y;
		acc3=accelerometerPacket.z;
		GPIO_ResetBits(GPIOD, GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14 | GPIO_Pin_15);
		if(accelerometer_x<-20){
		GPIO_ToggleBits(GPIOD, GPIO_Pin_13);
		}
		if(accelerometer_x>20){
		GPIO_ToggleBits(GPIOD, GPIO_Pin_15);
		}
		if(accelerometer_y<-20){
		GPIO_ToggleBits(GPIOD, GPIO_Pin_12);
		}
		if(accelerometer_y>20){
		GPIO_ToggleBits(GPIOD, GPIO_Pin_14);
		}

		VCP_send_buffer(&accelerometerPacket, sizeof(accelerometer_t));

		if(GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_0))
		        	buttonPacket.button_state=0xFF;
					else
					buttonPacket.button_state=0x00;

		VCP_send_buffer(&buttonPacket, sizeof(button_t));

	}

		return 0;
	}



void init()
{
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);

	GPIO_InitTypeDef  Diody;
	Diody.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13| GPIO_Pin_14| GPIO_Pin_15;
	Diody.GPIO_Mode = GPIO_Mode_OUT;
	Diody.GPIO_OType = GPIO_OType_PP;
	Diody.GPIO_Speed = GPIO_Speed_100MHz;
	Diody.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &Diody);

	/* Setup SysTick or CROD! */
	if (SysTick_Config(SystemCoreClock / 1000))
	{
		ColorfulRingOfDeath();
	}


	/* Setup USB */
	USBD_Init(&USB_OTG_dev,
	            USB_OTG_FS_CORE_ID,
	            &USR_desc,
	            &USBD_CDC_cb,
	            &USR_cb);

	return;
}

/*
 * Call this to indicate a failure.  Blinks the STM32F4 discovery LEDs
 * in sequence.  At 168Mhz, the blinking will be very fast - about 5 Hz.
 * Keep that in mind when debugging, knowing the clock speed might help
 * with debugging.
 */
void ColorfulRingOfDeath(void)
{
	uint16_t ring = 1;
	while (1)
	{
		uint32_t count = 0;
		while (count++ < 500000);

		GPIOD->BSRRH = (ring << 12);
		ring = ring << 1;
		if (ring >= 1<<4)
		{
			ring = 1;
		}
		GPIOD->BSRRL = (ring << 12);
	}
}

/*
 * Interrupt Handlers
 */

void SysTick_Handler(void)
{
	ticker++;
	if (downTicker > 0)
	{
		downTicker--;
	}
}

void NMI_Handler(void)       {}
void HardFault_Handler(void) { ColorfulRingOfDeath(); }
void MemManage_Handler(void) { ColorfulRingOfDeath(); }
void BusFault_Handler(void)  { ColorfulRingOfDeath(); }
void UsageFault_Handler(void){ ColorfulRingOfDeath(); }
void SVC_Handler(void)       {}
void DebugMon_Handler(void)  {}
void PendSV_Handler(void)    {}

void OTG_FS_IRQHandler(void)
{
  USBD_OTG_ISR_Handler (&USB_OTG_dev);
}

void OTG_FS_WKUP_IRQHandler(void)
{
  if(USB_OTG_dev.cfg.low_power)
  {
    *(uint32_t *)(0xE000ED10) &= 0xFFFFFFF9 ;
    SystemInit();
    USB_OTG_UngateClock(&USB_OTG_dev);
  }
  EXTI_ClearITPendingBit(EXTI_Line18);
}
