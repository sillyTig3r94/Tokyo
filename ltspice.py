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

from tool_database import *

from tool_analysis import *

# =============================================================================
#                       GETTING WORKING DIRECTORY PATH
# =============================================================================
path = os.getcwd()

asc_path = os.path.join(path,"BHA_setup_Extra.asc")

raw_path = os.path.join(path,"BHA_setup_Extra.raw")


LTC_BHA = LTCommander(asc_path)

database = import_tool_database()
# =============================================================================
#                       LTSPICE PARAMETER SETTING
# =============================================================================

# =============================================================================
#                       OLD LTSPICE CONFIGURATION
# =============================================================================
#tool_name, tool_sink, tool_source, tool_calc, tool_enable = get_tool_params()

#set_calc_setup(tool_name, tool_sink, tool_source, tool_calc, tool_enable)
    
#up_tools, dw_tools = lt_ult.set_params(tool_name)

#config source typ here

#config = lt_ult.set_params(tool_name)
# =============================================================================
#                       NEW LTSPICE CONFIGURATION
# =============================================================================
setting = import_tool_setting()

ltspice_setup(setting,database)
# =============================================================================
#                       RUN LTSPICE.ASC         
# =============================================================================
LTC_BHA.run()

LTR_BHA = LTSpiceRawRead(raw_path)

#export_tool_param()
#
#import_tool_param()

# =============================================================================
#                       COLLECTING DATA
# =============================================================================

LTC_BHA.__del__()

V,I,P,Generator,Battery  = lt_ult.get_tool(LTR_BHA,setting)

# =============================================================================
#                       OUTPUT CALCULATION
# =============================================================================
write_Report_to_Apps(V,I,P,Generator,Battery,setting)

write_Error_to_Apps(V,I,P,Generator,Battery,setting,database)





 



#
#
#
