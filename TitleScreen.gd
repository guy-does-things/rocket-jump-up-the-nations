extends Control

export(float) var move_speed = 5000

func _process(delta):
	$ParallaxBackground.scroll_offset.x += move_speed * delta
