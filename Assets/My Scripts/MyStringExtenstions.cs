﻿using System;
public static class MyStringExtenstions
{
    // Start is called before the first frame update
    public static string[] SplitStringWithString(string toEdit,string toSplitWith)
    {
        return toEdit.Split(new string[] { toSplitWith }, StringSplitOptions.None);
    }
}
