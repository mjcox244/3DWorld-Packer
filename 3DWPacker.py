import os, shutil

Mod_name = str(input("What is your mod name? "))
Mod_desc = str(input("Enter a short 1 line description of your mod... \n"))
Mod_path = str(input("Drag and drop the contents folder here! "))

#This code gets the curent directory of 
cwd = os.getcwd()
cwdlen = len(cwd)
cwdspaceless = ""
x = 0
for i in cwd:
	if x < cwdlen:
		cwdspaceless = cwdspaceless + i
	x += 1

#Makes the directory its going to output to
outputdir = "\"" + cwdspaceless + "\\" + Mod_name + "\\contents\""
print(outputdir)
os.system("mkdir " + outputdir)

#copy the contents folder
os.system("robocopy " + Mod_path + " " + outputdir + " /S")

#makes the rules.txt file
rules = "[Definition]\ntitleIds = 0005000010145D00,0005000010145C00,0005000010106100\nname = " + Mod_name + "\npath = Super Mario 3D World/Game Mods/" + Mod_name + "\ndescription = " + Mod_desc + " \nversion = 5"
output_path = cwdspaceless + "\\" + Mod_name + "\\rules.txt"
f = open(output_path, "a")
f.write(rules)
f.close()
