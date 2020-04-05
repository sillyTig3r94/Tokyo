# -*- coding: utf-8 -*-
"""
Created on Tue Mar 31 14:51:07 2020

@author: pisnm
"""

# =============================================================================
# 
# =============================================================================
from PyLTSpice.LTSpice_RawRead import LTSpiceRawRead

from PyLTSpice.LTSpiceBatch import *

import matplotlib.pyplot as plt

import numpy as np

import os

import ltspice_utilities as lt_ult

from tools_param import *



# =============================================================================
#                       GETTING WORKING DIRECTORY PATH
# =============================================================================
path = os.getcwd()

asc_path = os.path.join(path,"BHA_setup.asc")

raw_path = os.path.join(path,"BHA_setup.raw")



# =============================================================================
#                       PARAMETER SETTING
# =============================================================================

tools_setup = get_tool_params()

LTC_BHA = LTCommander(asc_path)

up_tools, dw_tools = lt_ult.set_params(tools_setup)

#up_tools, dw_tools = lt_ult.set_params(['Yoko','Brussels','Muscat','ESRX1','RioBase','ESTX','GP9600'])

# =============================================================================
#                       RUN LTSPICE.ASC         
# =============================================================================

LTC_BHA.run()

LTR_BHA = LTSpiceRawRead(raw_path)

# =============================================================================
#                       COLLECTING DATA
# =============================================================================
voltage_uh, current_uh, voltage_dh, current_dh  = lt_ult.get_tool(LTR_BHA)

# =============================================================================
#                       OUTPUT CALCULATION
# =============================================================================
write_to_Apps(voltage_dh,current_dh,dw_tools,Mode = 'down')
write_to_Apps(voltage_uh,current_uh,up_tools,Mode = 'up')

 



#
#
#
