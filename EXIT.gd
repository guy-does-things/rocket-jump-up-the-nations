extends TileMap


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_Area2D_body_entered(body):
	if body.is_in_group("player"):
		var pene = preload("res://LevelCleared.tscn").instance()
		Globals.TheWorld.endscreen.add_child(pene)
		get_tree().paused = true
		queue_free()
