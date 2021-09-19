extends Label
class_name Timerlabel

var current_time : float


func _ready():
	align = Label.ALIGN_CENTER

func _physics_process(delta):
	current_time += delta
	
	text = str("TIME: ", stepify(current_time, 0.1) )


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
