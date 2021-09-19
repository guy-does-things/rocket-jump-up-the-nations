extends Node


var currentlevel = 0



var levels = {
	1:"res://Levels/level1.tscn",
	2:"res://Levels/level2.tscn",
	3:"res://Levels/level3.tscn"
}


func increaseLevel():
	currentlevel += 1
	
	get_tree().change_scene(levels[currentlevel])
	


func forcelevel(newlevel):
	currentlevel = newlevel
	get_tree().change_scene(levels[currentlevel])
