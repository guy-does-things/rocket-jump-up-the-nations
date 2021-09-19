extends Node


enum INPUT_TYPE {
	KB,
	MOUSE
}

const SAVE_PATH = "user://config.dat"

const DEFAUT_SETTINGS = {
	"buttons":
		{
			"left":[INPUT_TYPE.KB, 65],
			"right":[INPUT_TYPE.KB, 68],
			"jump":[INPUT_TYPE.KB, 32],
			"shoot":[INPUT_TYPE.MOUSE, 1],
			"hook":[INPUT_TYPE.MOUSE, 2]
			
		}
}


var settings = {}


func _ready():
	settings = DEFAUT_SETTINGS
	load_config()
	
	load_input()



func SAVE_CONFIG():
	var file :File = File.new()
	file.open(SAVE_PATH, file.WRITE)
	file.store_var(settings)
	file.close()


func load_config():
	var file :File = File.new()
	if file.file_exists(SAVE_PATH):
		file.open(SAVE_PATH, file.READ)
		settings = file.get_var(true)
		file.close()
		return false
		
	return true


func load_input():
	for input in settings.buttons:
		var input_thing = settings["buttons"][input]

		match input_thing[0]:
			INPUT_TYPE.KB:
				var key = InputEventKey.new()
				key.scancode = input_thing[1]
				change_input(input, key)
				
			INPUT_TYPE.MOUSE:
				var key = InputEventMouseButton.new()
				key.button_index = input_thing[1]
				change_input(input, key)
	
	


func erase_controls(action_name : String):
	InputMap.action_erase_events(action_name)



func save_input_change(action_name : String, action_to_save :InputEvent):
	if action_to_save is InputEventKey:
		settings["buttons"][action_name] = [INPUT_TYPE.KB, action_to_save.scancode]
	elif action_to_save is InputEventMouseButton:
		settings["buttons"][action_name] = [INPUT_TYPE.MOUSE, action_to_save.button_index]

	SAVE_CONFIG()

func change_input(action_name : String, new_action : InputEvent):
	erase_controls(action_name)
	save_input_change(action_name, new_action)
	InputMap.action_add_event(action_name, new_action)



func _input(event):
	if Input.is_action_just_pressed("ui_cancel"):
		get_tree().reload_current_scene()
		_ready()
		
	if Input.is_key_pressed(KEY_R):#is_action_just_pressed("ui_end"):
		get_tree().change_scene("res://Levels/level1.tscn")
	if Input.is_key_pressed(KEY_F):
		get_tree().change_scene("res://Settigns.tscn")
