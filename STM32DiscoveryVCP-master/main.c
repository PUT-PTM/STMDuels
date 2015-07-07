
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

volatile uint32_t ticker, downTicker;

float acc1, acc2, acc3;

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

 GPIO_InitTypeDef setup_diody() {
 	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);

 	GPIO_InitTypeDef  Diody;
 	Diody.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13| GPIO_Pin_14| GPIO_Pin_15;
 	Diody.GPIO_Mode = GPIO_Mode_OUT;
 	Diody.GPIO_OType = GPIO_OType_PP;
 	Diody.GPIO_Speed = GPIO_Speed_100MHz;
 	Diody.GPIO_PuPd = GPIO_PuPd_NOPULL;
 	GPIO_Init(GPIOD, &Diody);
 }

 setup_przycisk() {
 	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOA, ENABLE);
 	GPIO_InitTypeDef  Przycisk;
 	Przycisk.GPIO_Pin = GPIO_Pin_0;
 	Przycisk.GPIO_Mode = GPIO_Mode_IN;
 	GPIO_Init(GPIOA, &Przycisk);
 }


int main(void)
{
	/* Set up the system clocks */
	SystemInit();

	/* Initialize USB, IO, SysTick, and all those other things you do in the morning */
	init();

	SystemCoreClockUpdate();

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
	   	accelerometerPacket.start_flag=0xAA;
	   	accelerometerPacket.command=0xAC;
	   	accelerometerPacket.crc=0x00;

	   	/*inicjalizacja pakietu przycisku*/
	   	button_t buttonPacket;
	   	buttonPacket.start_flag=0xAA;
	   	buttonPacket.command=0x38;
	   	buttonPacket.crc=0x00;

	   	setup_przycisk();
	   	setup_diody();

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

		for(i=0;i<500000;i++){

			//petla opozniajaca

		        }

		accelerometerPacket.x=(accelerometer_x*9.8f)/128-0.25f;
		accelerometerPacket.y=(accelerometer_y*9.8f)/128-0.2f;
		accelerometerPacket.z=(accelerometer_z*9.8f)/128-3.5f;


		acc1=accelerometerPacket.x;
		acc2=accelerometerPacket.y;
		acc3=accelerometerPacket.z;


					GPIO_ToggleBits(GPIOD, GPIO_Pin_15);


					if(accelerometer_x>30){
						GPIO_ToggleBits(GPIOD, GPIO_Pin_12);
					}
					if(accelerometer_y>30){
						GPIO_ToggleBits(GPIOD, GPIO_Pin_13);
					}
					if(accelerometer_z>30){
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
