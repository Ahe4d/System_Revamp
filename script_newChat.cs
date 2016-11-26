
function clientCmdChatMessage(%cl, %voice, %pitch, %line, %pre, %name, %suf, %msg) {
    %f1 = "<font:georgia:"@16+$Pref::GUI::ChatSize@">";
    %f2 = "<font:georgia:"@14+$Pref::GUI::ChatSize@">";
    %words = getWordCount(%msg) + 1;
    for(%i=0;%i < %words;%i++) {
        if(getSubStr(getWord(%msg, %i), 0, 1) !$= "@" && (%a=strPos(getWord(%msg, %i), "@")) == -1)
            continue;
        if(%a != -1)
            %word = strReplace(getSubStr(getWord(%msg, %i), %a, strLen(getWord(%msg, %i))), "@", "");
        else
            %word = strReplace(getWord(%msg, %i), "@", "");
        if((%id = Chat_CheckIsClient(%word)) != -1)
             %msg = strReplace(%msg, "@" @ %word, "@" @ (%col=collapseEscape("\\c" @ getField(%id, 0))) @ %word @ ((%n=getField(%id, 1)) !$= "" ? " \c7[\c3" @ %col @ %n @ "\c7] " : "") @ "\c6");
    }
    echo(stripMlControlChars("-> \c7" @ (%p = (%pre !$= "" ? "\c7(" @ %pre @ "\c7)\c3 " : "\c3")) @ %name @ (%s = (%suf !$= "" ? "\c7(" @ %suf @ "\c7)" : "")) @ " \c6:" SPC %msg));
    %msg = "<font:georgia:20>\c7->" SPC %f2 @ "\c7" @ stripMlControlChars(%p) @ %f1 @ "\c3" @ %name @ "\c7" @ %f2 @ "\c7" SPC stripMlControlChars(%s) @ %f1 @ "\c6:" SPC "<font:georgia:"@14+$Pref::GUI::ChatSize@">\c6" @ %msg;
    NewChatHud_addLine(%msg);
}

function Chat_CheckIsClient(%text) {
    for(%i=0;%i < NPL_List.rowCount();%i++) {
        %field = NPL_List.getRowText(%i);
        if((%name = getField(%field, 1)) !$= "" && %text $= getSubStr(%name, 0, strLen(%text))) {
            if(%name $= $Pref::Player::NetName)
               return 4 @ "\t" @ "You";
            if(getField(%field, 0) $= "-")
                return 3 @ "\t" @ %name; // NORMAL
            if(getField(%field, 0) $= "A")
                return 2 @ "\t" @ %name; // ADMIN
            return 0 @ "\t" @ %name; // SA
        }
    }
    return -1;
}

function Chat_Checker() {
     if(!isEventPending($Chat_Checker))
          %firstTime = true;
     if(isEventPending($Chat_Checker)) {
          cancel($Chat_Checker);
          %firstTime = false;
     }
     if($Pref::GUI::ChatSize != $LastChatSize && !%firstTime)
          messageBoxOK("Looks like you were changing the chat...", "Please keep in mind, the chat will not auto change. Certain lines will, but System_Revamp will not.");
     $LastChatSize = $Pref::GUI::ChatSize;
     $Chat_Checker = schedule(1750, 0, Chat_Checker);
} Chat_Checker();

function getNormalDateTime() {
	%date = getWord(getDateTime(),0);
	%date = strReplace(%date,"/","-");
	%time = getSubStr(getDateTime(),9,2) / mFloor(2.5);
	%time = %time @ ":" @ getSubStr(getDateTime(),12,2);
	return %date SPC %time;
}

package newChat {
    function GameConnection::onConnectionAccepted(%this, %server) {
        parent::onConnectionAccepted(%this, %server);
        %date = getWord(getNormalDateTime(), 0);
        %time = getWord(getNormalDateTime(), 1);
        %name = NPL_Windows.text;
        echo("JOINED SERVER AT TIME: [" @ %time SPC %date @ "]" SPC %path);
    }
};
activatePackage(newChat);
