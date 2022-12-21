### main.py
import utime
import time
from machine import I2C,mem32,Pin,Timer
import _thread
#i2c slave essentials
from i2cSlave import i2c_slave
#ds18b20 essentials
import onewire, ds18x20
#ssd1603 essentials
from ssd1306 import SSD1306_I2C
import framebuf

### --- heating element, on/off pin --- ###
heating = Pin(18, Pin.OUT)
heating_debug = Pin(25, Pin.OUT)
heating.off()
heating_debug.off()

### --- pico connect i2c as slave --- ###
s_i2c = i2c_slave(0,sda=0,scl=1,slaveAddress=0x41)

### --- connecting SSD1306 display through I2C backpack --- ###
i2c = I2C(1, scl=Pin(15), sda=Pin(14), freq=200000)
oled = SSD1306_I2C(128, 64, i2c)

### --- sponsor of the hardware --- ###
oled.fill(0)
oled.invert(1)
oled.text("ILYA",31,15,1)
oled.text("(tm)",60,12,1)
oled.text("Heavy Industries",1,30,1)
oled.show()
time.sleep(5)
oled.invert(0)

### --- global variables --- ###
PI_COMMAND = 0;
PICO_TEMPERATURE = 0;

### --- a semaphore (A.K.A lock) --- ### 
baton = _thread.allocate_lock()

def Pi4ReaderWriter():
    global PI_COMMAND
    global PICO_TEMPERATURE
    while True:
        if s_i2c.any():
            pi_command = int(f'{s_i2c.get():08b}', 2)
            baton.acquire()
            PI_COMMAND = pi_command
            pico_temperature = PICO_TEMPERATURE
            baton.release()
        if s_i2c.anyRead():
            baton.acquire()
            pico_temperature = int((round(PICO_TEMPERATURE * 2) / 2) * 2)
            baton.release()
            s_i2c.put(pico_temperature & 0xff)
        
### --- ds18b20 search --- ### 
ds_pin = machine.Pin(16)
ds_sensor = ds18x20.DS18X20(onewire.OneWire(ds_pin))
roms = ds_sensor.scan()

### --- MAIN MASSACRE --- ###
_thread.start_new_thread(Pi4ReaderWriter, ())

try:
    heating_state = -1
    while True:
        oled.fill(0)
        
        ds_sensor.convert_temp()
        time.sleep_ms(750)
        
        baton.acquire()
        PICO_TEMPERATURE = ds_sensor.read_temp(roms[0])
        pico_temperature = PICO_TEMPERATURE
        pi_command = PI_COMMAND
        baton.release()
        
        oled.text("Current t: {} C".format(round(pico_temperature, 2)),1,15,1)
        #oled.text("modded t: {} C".format(int((round(pico_temperature * 2) / 2) * 2) & 0xff),1,45,1)
        
        if pi_command == 0:
            oled.text("Heating: off",1,30,1)
            if heating_state == -1 or heating_state == 1:
                heating.off()
                heating_state = 0
                heating_debug.off()
                    
        else:
            oled.text("Heating: {} C".format(pi_command/2),1,30,1)
            if pico_temperature < ((pi_command/2) - 0.5):
                if heating_state == -1 or heating_state == 0:
                    heating.on()
                    heating_state = 1
                    heating_debug.on()
            if pico_temperature >= ((pi_command/2)):
                if heating_state == -1 or heating_state == 1:
                    heating.off()
                    heating_state = 0
                    heating_debug.off()
            
        oled.show()
        time.sleep(0.50)

except KeyboardInterrupt:
    pass