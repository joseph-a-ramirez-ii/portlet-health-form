    
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_SIMPLEQUERY_CELL_PADDING_LABEL' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
    VALUES ('CUS_SIMPLEQUERY_CELL_PADDING_LABEL', 'En','Results Grid Cell Padding (pixels)', null);

IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_SIMPLEQUERY_CELL_PADDING_DESC' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
    VALUES ('CUS_SIMPLEQUERY_CELL_PADDING_DESC', 'En','The space between the contents of each cell of the results grid and the boundary of that cell. Value should be an integer representing a number of pixels.', null);





---------
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_HEALTHFORM_ACCESS_DENIED_MESSAGE' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
    VALUES ('CUS_HEALTHFORM_ACCESS_DENIED_MESSAGE','En','Health Form for students only',null)
    
IF NOT EXISTS(SELECT Text_Key FROM FWK_Globalization WHERE Text_Key='CUS_HEALTHFORM_ACCESS_DENIED_MESSAGE' AND Language_Code='En')
    INSERT INTO FWK_Globalization (Text_Key, Language_Code, Text_Value,Text_Custom_Value)
    VALUES ('CUS_HEALTHFORM_WELCOME_MESSAGE','En','Health Form for students only',null)