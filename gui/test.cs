//Since I'm not really overwriting anything here,
//there's no need to put the entire Gui here. Just a plain .cs file.

function ProgressGUI::onWake(%this)
{
   Progress_Window.position = "0 -800";
   Progress_Window.tangoMoveTo("centerY", "1000", "elastic");
}

function ProgressGUI::onSleep(%this)
{
   Progress_Window.position = "0 -800";
}