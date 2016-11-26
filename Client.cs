echo("--| ...System_Revamp being delt with... |---");
if(!isObject(NewCursorGui))
    exec("./NewCursorGui.gui");
if(isObject(NetGraphGui)) {
    NetGraphGui.delete();
    exec("./NetGraphGui.gui");
}
if(isObject(ConsoleDlg)) {
    consoleDlg.delete();
    exec("./ConsoleDlg.gui");
}
if(!isObject(hudlessGui))
    exec("./hudlessGui.gui");
if(!isFunction(GuiControl, TangoMoveTo))
    exec("./tango.cs");

exec("./script_newChat.cs");

if(!isObject(NewCursorButton)) {
    %button = new GuiButtonCtrl(NewCursorButton) {
        text = "Set new cursor";
        position = "241 333";
        command = "canvas.pushDialog(\"NewCursorGui\");";
    };
    if(isObject(OptGraphicsPane))
        OptGraphicsPane.add(%button);
}

function ResetCursorDefaults() { // This is to forcefully make sure our cursor doesn't delete itself.
    DefaultCursor.delete(); // If it exists, delete it, then reset it.
    if($platform $= "macos") {
        new GuiCursor(DefaultCursor ) {
            hotSpot = "4 4";
            renderOffset = "0 0";
            bitmapName = "./cursor/cursor" @ $Pref::Revamp::Cursor;
        };
    } else {
        new GuiCursor(DefaultCursor) {
            hotSpot = "1 1";
            renderOffset = "0 0";
            bitmapName = "./cursor/cursor" @ $Pref::Revamp::Cursor;
        };
    }
    Canvas.setCursor(DefaultCursor); // Set the cursor to the cursor you've set.
}

function newCursorGui::onWake(%this)
{
    //%centerX = getWord($pref::Video::resolution,1) / 2 - getWord(%this.extent,1) / 2;
    %p = $Pref::Video::Resolution;
    CursorGui.position = getRandom(-900 - %p, 900 + %p) SPC getRandom(-900 - %p, 900 + %p);
    CursorGui.tangoMoveTo("center", "1000", "elastic");
}

function newCursorGui::onSleep(%this)
{
    //%centerX = getWord($pref::Video::resolution,1) / 2 - getWord(%this.extent,1) / 2;
    %p = $Pref::Video::Resolution;
    CursorGui.position = getRandom(-900 - %p, 900 + %p) SPC getRandom(-900 - %p, 900 + %p);
}

function NewCursorGui::setCursor(%this, %cursor)
{
    %f = new FileObject();
    %f.openForWrite("config/revamp.txt");
    %f.writeLine("Cursor " @ %cursor);
    %f.close(); %f.delete();
    echo("Attempting to set cursor to" SPC $Pref::Revamp::Cursor = %cursor);
    ResetCursorDefaults();
}

package Revamp {
    function loadMainMenu() {
    	parent::loadMainMenu();
        if(isFile("config/revamp.txt")) {
            %f = new FileObject();
            %f.openForRead("config/revamp.txt");
            $Pref::Revamp::Cursor = getWord(%f.readLine(), 1);
            %f.close(); %f.delete();
            ResetCursorDefaults();
        }
    }
};
activatePackage(Revamp);
