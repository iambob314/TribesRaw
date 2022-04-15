
for (%f = File::findFirst("strings\\*.strings.cs"); %f != ""; %f = File::findNext("strings\\*.strings.cs")) exec2(%f);
