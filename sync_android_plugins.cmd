SETLOCAL

set SOURCE_FILE_PATH=%~dp0AndroidProject\unisharper-core\build\outputs\aar\unisharper-core-debug.aar
set NEW_FILE_PATH=%~dp0Assets\UniSharper.Core\Plugins\Android\unisharper-core.aar

echo "Synchronizing Android plugin of UniSharper.Core..."

del /f /q "%NEW_FILE_PATH%"
copy /y %SOURCE_FILE_PATH% %NEW_FILE_PATH%

ENDLOCAL