//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//----------------------The rest of this wasn't needed-------------------------
if( $Pref::Cursor !$= "" ) //Did you have a previous version?
	$Pref::Revamp::Cursor = $Pref::Cursor;

if( !$Pref::Cursor ) //did you already set a default cursor?
   $Pref::Cursor = 0; //make it default.

if( isObject( DefaultCursor ) ) //does it already exist?
   DefaultCursor.delete(); //delete it.

//what's your platform?
if( $platform $= "macos" )
{
   new GuiCursor (DefaultCursor )
   {
      hotSpot = "4 4";
      renderOffset = "0 0";
      bitmapName = "./images/cursor" @ $Pref::Revamp::Cursor;
   };
} 
else 
{
   new GuiCursor( DefaultCursor )
   {
      hotSpot = "1 1";
      renderOffset = "0 0";
      bitmapName = "./images/cursor" @ $Pref::Revamp::Cursor;
   };
}

function toggleNewCursor(%key)
{
   if(%key) //was it pressed?
   {
      if(newCursorGui.isAwake)
         return canvas.popDialog(newCursorGui);
      else
         return canvas.pushDialog(newCursorGui);
   }
   //else
      //return //was it released?
}

function OptionsDlg::applyGraphics(%this, %testNeedApply)
{
   parent::applyGraphics(%this, %testNeedApply);
   defaultCursor.delete();
   exec("./cursor.cs"); //it'll delete your cursor if you don't do this.
}