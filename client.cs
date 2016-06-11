
$Revamp::Version = 0.71;

if(!$Revamp::exec) {
	//---Smooth movement; Tango. by Zapk---
	exec("./tango.cs");

	//---GUI---
	exec("./gui/adminGui.gui");
	exec("./gui/RevampWelcomeDlg.gui");
	exec("./gui/ConsoleDlg.gui");
	exec("./gui/netGraphGui.gui");
	exec("./gui/newCursorGui.gui");
	exec("./gui/hudlessGui.gui");
	exec("./gui/test.cs");
	exec("./cursor.cs");
	globalActionMap.bind(keyboard, "Control-f5", toggleNewCursor);
	globalActionMap.bind(keyboard, "f6", ToggleHudlessPlayGui);

	if($Pref::Revamp::Version !$= $Revamp::Version || !$Pref::Revamp::FirstTime) {
		$Pref::Revamp::FirstTime = 0;
		schedule(1000, 0, RevampWelcome);
	}
	else
		RevampWelcomeDlg.delete(); //I'm not gonna clutter Gui's into Blockland.
}

function RevampWelcome() {
	$Pref::Revamp::FirstTime = 1;
	$Pref::Revamp::Version = $Revamp::Version;
	canvas.pushDialog(RevampWelcomeDlg);

	export("$Pref*","config/client/prefs.cs");
}

function loadMainMenu(%a,%b,%c,%d) {
	canvas.pushDialog(MainMenuGui);
	MainMenuGui.showButtons();
}
