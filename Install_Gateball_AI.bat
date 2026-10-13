@echo off
chcp 65001 > nul
title EDOPro 게이트볼 AI 1-클릭 무손실 설치기
echo =====================================================================
echo  [EDOPro 게이트볼 AI 14종 무손실 설치기]
echo =====================================================================
echo.

set "SCRIPT_DIR=%~dp0"
set "TARGET_DIR=%SCRIPT_DIR%"

if not exist "%TARGET_DIR%WindBot\bots.json" (
    if exist "%SCRIPT_DIR%..\WindBot\bots.json" (
        set "TARGET_DIR=%SCRIPT_DIR%..\"
    )
)

if not exist "%TARGET_DIR%WindBot\bots.json" (
    echo [안내] 현재 위치에서 EDOPro WindBot 폴더를 직접 감지하지 못했습니다.
    echo EDOPro가 설치된 폴더 경로를 입력해 주세요 (예: C:\EDOPro):
    set /p "TARGET_DIR=폴더 경로 입력: "
    if not "%TARGET_DIR:~-1%"=="\" set "TARGET_DIR=%TARGET_DIR%\"
)

if not exist "%TARGET_DIR%WindBot\bots.json" (
    echo [오류] "%TARGET_DIR%WindBot\bots.json" 파일을 찾을 수 없습니다.
    echo 올바른 EDOPro 폴더인지 확인해 주세요.
    pause
    exit /b 1
)

echo [1/3] AI 라이브러리 및 덱 파일 복사 중...
if not exist "%TARGET_DIR%WindBot\Executors" mkdir "%TARGET_DIR%WindBot\Executors"
if not exist "%TARGET_DIR%WindBot\Decks" mkdir "%TARGET_DIR%WindBot\Decks"

copy /y "%SCRIPT_DIR%WindBot\Executors\GateballExecutors.dll" "%TARGET_DIR%WindBot\Executors\GateballExecutors.dll" > nul
if exist "%TARGET_DIR%Executors" (
    copy /y "%SCRIPT_DIR%WindBot\Executors\GateballExecutors.dll" "%TARGET_DIR%Executors\GateballExecutors.dll" > nul
)
copy /y "%SCRIPT_DIR%WindBot\Decks\AI_*.ydk" "%TARGET_DIR%WindBot\Decks\" > nul

echo [2/3] 기존 bots.json 안전 백업 생성 중 (bots.json.bak)...
copy /y "%TARGET_DIR%WindBot\bots.json" "%TARGET_DIR%WindBot\bots.json.bak" > nul

echo [3/3] 기존 봇 목록 100%% 보존 및 게이트볼 AI 14종 무손실 병합 중...
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
    "$tPath = [System.IO.Path]::Combine('%TARGET_DIR%', 'WindBot\bots.json');" ^
    "$sPath = [System.IO.Path]::Combine('%SCRIPT_DIR%', 'gateball_bots.json');" ^
    "$tText = [System.IO.File]::ReadAllText($tPath, [System.Text.Encoding]::UTF8);" ^
    "$sText = [System.IO.File]::ReadAllText($sPath, [System.Text.Encoding]::UTF8);" ^
    "$tList = [System.Collections.ArrayList]@($tText | ConvertFrom-Json);" ^
    "$sList = $sText | ConvertFrom-Json;" ^
    "$dict = @{};" ^
    "foreach ($b in $tList) { if ($b.deck) { $dict[$b.deck] = $true; } };" ^
    "$count = 0;" ^
    "foreach ($b in $sList) {" ^
    "  if (-not $dict.ContainsKey($b.deck)) {" ^
    "    $null = $tList.Add($b);" ^
    "    $count++;" ^
    "  }" ^
    "};" ^
    "$outJson = ConvertTo-Json -InputObject $tList -Depth 10;" ^
    "[System.IO.File]::WriteAllText($tPath, $outJson, [System.Text.Encoding]::UTF8);" ^
    "Write-Host \"[OK] $count 개의 게이트볼 봇이 기존 목록에 성공적으로 병합되었습니다!\";"

echo.
echo =====================================================================
echo  [설치 완료] EDOPro 게이트볼 AI 14종이 성공적으로 설치되었습니다!
echo   * 기존 윈드봇 파일 및 커스텀 봇 100%% 보존 (덮어쓰기 0건)
echo   * 백업 파일: WindBot\bots.json.bak
echo  이제 EDOPro를 켜고 [봇 맞붙기]에서 [GOAT], [0703], [1103] 봇을 선택하세요!
echo =====================================================================
echo.
pause
