@echo Off
SET SolutionDir=%1
SET ProjectDir=%2

SET folder=%CD%
CD %SolutionDir%
SET SolutionDir=%CD%
CD %ProjectDir%
SET ProjectDir=%CD%
CD %folder%

SET ToolsDir=%~dp0
IF "%ProjectDir%" =="" SET ProjectDir=%ToolsDir%\..\..\..
IF "%SolutionDir%" =="" SET SolutionDir=%ToolsDir%\..\..\..
SET OutputDir=%ProjectDir%\Properties\Localization
SET ProjectFiles="%ToolsDir%\projectfiles.txt"
SET PotMessages="%OutputDir%\newmessages.pot"
SET InputExtensions=
FOR /f "delims=" %%i in ('type "%ToolsDir%\FileExtensions.txt"') do (
SET InputExtensions=%%i
GOTO Program
)


:Program

SET folder=%CD%
CD "%SolutionDir%"
dir %InputExtensions% /S /B > %ProjectFiles%
CD "%folder%"


@rem Create a new .pot from source, place it in the English folder, and merge with the existing .po file in English

IF NOT EXIST "%OutputDir%" mkdir "%OutputDir%"

@rem echo xgett
SET GNU=%ToolsDir%\..\..\GNU.Tools.1.0.0.0\tools
"%GNU%\xgettext.exe" -k -k_ -k__q -k__ -k___ -k___q -kDisplayName -kDisplayNameAttribute -kRange -kRangeAttribute -kRequired -kRequiredAttribute -kRegularExpression -kRegularExpressionAttribute -kStringLength -kStringLengthAttribute -kCompare -kCompareAttribute -kEmail -kEmailAttribute -kGetText -kGetString -kGetHtml -kGetRaw -kFormatHtml -kFormatRaw -kGetRaw -kFormatRaw -k__h -k_s:1,2 -k__s:1,2 -k__hs:1,2 --from-code=UTF-8 -LC# --omit-header -o%PotMessages% -f%ProjectFiles% --language=C# --sort-by-file
IF NOT EXIST %PotMessages% ECHO #No items found > %PotMessages%
IF NOT EXIST "%OutputDir%\messages.po" TYPE %PotMessages% >"%OutputDir%\messages.po"

"%GNU%\msgmerge.exe" --backup=none -U "%OutputDir%\messages.po" %PotMessages% -q

@rem echo Merge the newly created .pot file with the Other Language translations:

FOR /f "delims=" %%i in ('type "%ToolsDir%\Languages.txt"') do (
	if not exist "%OutputDir%\messages.%%i.po" (
		echo # PO Localization Tool for Visual Studio 2010, by laurentiu.macovei@dotnetwise.com, 2011.									> "%OutputDir%\messages.%%i.po"
		echo msgid ""									 >>"%OutputDir%\messages.%%i.po"
		echo msgstr ""									 >>"%OutputDir%\messages.%%i.po"
		echo "Language: %%i\n"							 >>"%OutputDir%\messages.%%i.po"
		echo "MIME-Version: 1.0\n"						 >>"%OutputDir%\messages.%%i.po"
		echo "Content-Type: text/plain; charset=UTF-8\n" >>"%OutputDir%\messages.%%i.po"
		echo "Content-Transfer-Encoding: 8bit\n"		 >>"%OutputDir%\messages.%%i.po"
		type %PotMessages% >> "%OutputDir%\messages.%%i.po"
	)
	"%GNU%\msgmerge.exe" --backup=none -U "%OutputDir%\messages.%%i.po" %PotMessages% --quiet --sort-by-file --lang=%%i
)

DEL /Q /F %ProjectFiles%