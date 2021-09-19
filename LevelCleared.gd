extends Control


func _ready():
	$CenterContainer/VBoxContainer/Label2.text = str("DIED ", Globals.TheWorld.get_TimesDead(), " TIMES THIS LEVEL")
	$CenterContainer/VBoxContainer/Label3.text = str("DIED ", Globals.get_TimesDead(), " TIMES")
	
	if Levels.currentlevel + 1 >= 4:
		$CenterContainer/VBoxContainer/Label.text = "you won"
		$CenterContainer/VBoxContainer/Button.hide()
		
func _on_Button_pressed():
	get_tree().paused = false
	
	Levels.increaseLevel()


func _on_Button2_pressed():
	get_tree().paused = false
	Levels.currentlevel = 0
	Globals.Restart()
	get_tree().change_scene("res://TitleScreen.tscn")
