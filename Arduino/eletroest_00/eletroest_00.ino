#include "pwm_lib.h"

using namespace arduino_due::pwm_lib;

uint32_t period = 1000000; // 10ms (em 0,01 ns)
uint32_t freq = 0;
uint32_t duty = 0;  // Deve ser transformado em unidades de 0,01 ns também

uint8_t percent = 20;

long time_past = 0;

pwm<pwm_pin::PWMH1_PA19> pwm_pin42;  //Pino 42

void setup()
{
    duty = (unsigned long) map(percent, 0, 100, 0, period);
    pwm_pin42.start(period, duty);
}
void loop()
{
    if(millis() - time_past > 5000)
    {
        time_past = millis();
        percent += 10;
        
        period /= 2;
        duty = (unsigned long) map(percent, 0, 100, 0, period);

        pwm_pin42.set_period_and_duty(period, duty);
        
        if(percent >= 100)  percent = 0;
    }
}
