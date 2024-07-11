@echo off
(
echo y
echo y
) | python "D:\GitHub\team3_work\projectFiles\DatabaseProject\bin\Debug\..\..\..\Instant-NGP\scripts\colmap2nerf.py" --video_in "D:\GitHub\team3_work\projectFiles\DatabaseProject\bin\Debug\..\..\..\videos\chiikawa\chiikawa.mp4" --run_colmap --out "D:\GitHub\team3_work\projectFiles\DatabaseProject\bin\Debug\..\..\..\videos\chiikawa\transforms.json"
exit