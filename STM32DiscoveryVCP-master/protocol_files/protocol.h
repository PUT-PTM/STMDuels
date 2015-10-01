#include <stdlib.h>
#include <stdio.h>
#include "misc.h"

typedef struct button{
	//command type 0x38
	uint8_t start_flag;
	uint8_t command;
	uint8_t button_state;
	uint8_t crc;
}button_t;

typedef struct accelerometer{
	//command type 0xAC
	uint8_t start_flag;
	uint8_t command;
	int16_t x;
	int16_t y;
	int16_t z;
	uint8_t crc;

}accelerometer_t;
