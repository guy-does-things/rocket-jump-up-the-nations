#tool
extends HBoxContainer

export(String) var input_name
var started = false

# Called every frame. 'delta' is the elapsed time since the previous frame.

func get_input_name(event)->String:
	if event is InputEventMouseButton:
		return "Mouse Button" + str(event.button_index)
		
	print(OS.get_scancode_string(event.scancode))
	return OS.get_scancode_string(event.scancode)
	
func _process(delta):
	
	$Label.text = input_name + " " + get_input_name(InputMap.get_action_list(input_name)[0])

	if !started:
		set_process_input(false)
		$Button.text = "press to change input"
		started = true

func _input(event):
	if !event is InputEventMouseMotion:
		
		if event is InputEventKey or event is InputEventMouseButton:
			Settings.change_input(input_name, event)
		else:
			set_process_input(false)
			$Button.text = "sorry, controllers or whatever you tried to input not supported :(, was too lazy"
			$Timer.start()
			yield($Timer, "timeout")
			$Button.text = "press to change input"
		
		set_process_input(false)
		

func _on_Button_pressed():
	set_process_input(true)
	
