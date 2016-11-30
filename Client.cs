$System_RevampVersion = "0.27-1 ALPHA stable";

echo("\n--| ...System_Revamp being delt with... |--");
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

function resetCursorDefaults() { // This is to forcefully make sure our cursor doesn't delete itself.
    if(isObject(DefaultCursor))
        DefaultCursor.delete();
    if($platform $= "macos") {
        new GuiCursor(DefaultCursor) {
            hotSpot = "4 4";
            renderOffset = "0 0";
            bitmapName = "./cursor/" @ $Pref::Revamp::Cursor;
        };
    } else {
        new GuiCursor(DefaultCursor) {
            hotSpot = "1 1";
            renderOffset = "0 0";
            bitmapName = "./cursor/" @ $Pref::Revamp::Cursor;
        };
    }
    Canvas.setCursor(DefaultCursor); // Set the cursor to the cursor you've set.
}

function GuiWindowCtrl::MoveToCenter(%this) {
    %this.position = getRandom(-900 - (%p = $Pref::Video::Resolution), 900 + %p) SPC getRandom(-900 - %p, 900 + %p);
    %this.tangoMoveTo("center", "1000", "elastic");
}

function newCursorGui::onWake(%this) {
    %n = "./cursor/*.png";
    %position = "1 1";
    for(%file=findFirstFile(getWord(%n, %i));%file !$= "";%file=findNextFile(getWord(%n, %i))) {
        %name = getSubStr(%file, strPos(%file, "_") + 1, strPos(%file, ".") - strPos(%file, "_") - 1);
        %id = getSubStr(%file, strPos(%file, "_") - 1, 1);
        %rad = new GuiRadioCtrl(cusorTemp);
        CursorGuiScroll.add(%rad);
        %rad.position = %position;
        %rad.setName("cursor" @ %id);
        %rad.command = "NewCursorGui::setCursor(\"Cursor" @ %id @ "_" @ %name @ "\");";
        %rad.text = %name;
        %position = "1" SPC getWord(%position, 1) + 20;
        NewCursorGui.cursorIds++;
    }
    CursorGuiHelp.command = "messageBoxOK(" @
        "\"System_Revamp\'s guide to cursors\"," @
        "\"To add a cursor to this list, make a png file, add it to \'S_Revamp.zip/cursor/\', and name it as such: CursorID_YourCursorName.png\");";
    CursorGui.MoveToCenter();
}

function newCursorGui::onSleep(%this) {
    for(%i=0;%i < NewCursorGui.cursorIds;%i++) {
        if(isObject(%cursor = cursor@%i))
            %cursor.delete();
    }
    NewCursorGui.cursorIds=0;
    CursorGui.position = getRandom(-900 - (%p = $Pref::Video::Resolution), 900 + %p) SPC getRandom(-900 - %p, 900 + %p);
}

function NewCursorGui::setCursor(%cursor) {
    %f = new FileObject();
    %f.openForWrite("config/revamp.txt");
    %f.writeLine(%cursor);
    %f.close(); %f.delete();
    echo("Attempting to set cursor to" SPC $Pref::Revamp::Cursor = %cursor);
    resetCursorDefaults();
}

package Revamp {
    function loadMainMenu() {
    	parent::loadMainMenu();
        if(isFile("config/revamp.txt")) {
            %f = new FileObject();
            %f.openForRead("config/revamp.txt");
            $Pref::Revamp::Cursor = %f.readLine();
            %f.close(); %f.delete();
            ResetCursorDefaults();
        }
    }
};
activatePackage(Revamp);

echo("--| ...System_Revamp finished loading successfuly! |--\n");
