tool
extends HBoxContainer

export(String) var input_name
var started = false

# Called every frame. 'delta' is the elapsed time since the previous frame.

func _process(delta):
	$Label.text = input_name + " " + str(InputMap.get_action_list(input_name))
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
	
