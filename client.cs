echo("--| System_Revamp being delt with... |---");
if(!isObject(NewCursorGui))
    exec("./NewCursorGui.gui");
if(isObject(NetGraphGui)) {
    NetGraphGui.delete();
    exec("./NetGraphGui.gui");
}
if(!isObject(hudlessGui))
    exec("./hudlessGui.gui");
if(!isFunction(GuiControl, TangoMoveTo))
    exec("./tango.cs");

if(!isObject(NewCursorButton)) {
    %button = new GuiButtonCtrl(NewCursorButton) {
        text = "Set new cursor";
        position = "241 333";
        command = "canvas.pushDialog(\"NewCursorGui\");";
    };
    if(isObject(OptGraphicsPane))
        OptGraphicsPane.add(%button);
}

function ResetCursorDefaults() {
    DefaultCursor.delete();
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
    Canvas.setCursor(DefaultCursor);
}

function newCursorGui::onWake(%this)
{
    %centerX = getWord($pref::Video::resolution,1) / 2 - getWord(%this.extent,1) / 2;
    CursorGui.position = %centerX SPC getRandom(-900, 900);
    CursorGui.tangoMoveTo("center", "1000", "elastic");
}

function newCursorGui::onSleep(%this)
{
    %centerX = getWord($pref::Video::resolution,1) / 2 - getWord(%this.extent,1) / 2;
    CursorGui.position = %centerX SPC getRandom(-900, 900);
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
