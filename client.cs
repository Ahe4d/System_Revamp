//System_Revamp.
$Revamp::Version = 0.7 @ "a"; //A for Alpha.

//I wanted to make it a bit easier for people to do things
//So I made this.
if(!$Revamp::exec)
{ //let's begin by echoing what this is.
	echo("\c5System_REVAMP, created by Anthonyrules144. \c6version" SPC $Revamp::Version);
	echo("Credit to GarageGames, and Zapk for making some awesome scripts!");
	echo("\c2---|\c5ATTEMPTING TO LOAD\c2|--- ");
	//---Smooth movement; Tango. by Zapk---
	exec("./tango.cs");

	//---GUI---
	exec("./gui/adminGui.gui"); //An edited GUI of what the default Admin Gui is. vForgotton
	exec("./gui/RevampWelcomeDlg.gui");
	exec("./gui/ConsoleDlg.gui"); //Fixed Console Dlg and added tango movement v0.7a
	exec("./gui/netGraphGui.gui"); //Torque3D's version of the Net Graph v0.41
	exec("./gui/newCursorGui.gui"); //Spice it up with some new cursors for the player v0.2a
	exec("./gui/hudlessGui.gui"); //Torque3D's "hudless PlayGui" -- v0.1 [the start of System_Revamp]

	exec("./gui/progress.cs"); //You're not a real Gui!!
	//---CURSOR Control---
	exec("./cursor.cs");

	echo("\c2---|\c5Loaded guis!\c2|---");

	//---SKYBOX--- wip
	//exec("./sky/skies.dml");

	globalActionMap.bind(keyboard, "f5", toggleNewCursor);
	globalActionMap.bind(keyboard, "f6", ToggleHudlessPlayGui);

	//canvas.popDialog(newCursorGui); //I have no clue why this happens

	echo("\c2---|\c5Attempting to check for first timer?\c2|---");

	if($Pref::Revamp::Version !$= $Revamp::Version || !$Pref::Revamp::FirstTime)
	{
		$Pref::Revamp::FirstTime = 0;
		schedule(1000, 0, RevampWelcome);
	}
	else
		RevampWelcomeDlg.delete(); //I'm not gonna clutter Gui's into Blockland.

	$Revamp::exec = 1;
	echo("\c2---|\c5Finished!\c2|---");
}

function RevampWelcome()
{
	$Pref::Revamp::FirstTime = 1;
	$Pref::Revamp::Version = $Revamp::Version;
	canvas.pushDialog(RevampWelcomeDlg);

	export("$Pref*","config/client/prefs.cs");
}

function loadMainMenu(%a,%b,%c,%d) //Let's load up the main menu.
{
	canvas.pushDialog(MainMenuGui); //We need the main menu don't we?
	MainMenuGui.showButtons(); //Show all the Main Menu buttons.
}

function OptionsDlg::applyGraphics(%this, %testNeedApply) //make sure we don't mess anything up.
{
	defaultCursor.delete(); //Delete the current
	exec("./cursor.cs"); //And bring it back again.
	parent::applyGraphics(%this, %testNeedApply); //parent it so nothing is messed.
}