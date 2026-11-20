extends Node3D

const EconomyScript = preload("res://scripts/economy.gd")

var economy: NationalEconomy
var game_day: int = 1
var game_month: int = 1
var game_year: int = 1
var speed_multiplier: float = 1.0
var simulation_timer: Timer
var camera_3d: Camera3D
var budget_label: Label
var debt_label: Label
var gdp_label: Label
var unemployment_label: Label
var inflation_label: Label
var date_label: Label
var touch_positions: Dictionary = {}
var previous_pinch_distance: float = 0.0

func _ready() -> void:
	randomize()
	economy = EconomyScript.new()
	_build_world()
	_build_interface()
	simulation_timer = Timer.new()
	simulation_timer.wait_time = 8.0
	simulation_timer.timeout.connect(_advance_day)
	add_child(simulation_timer)
	simulation_timer.start()
	_refresh_dashboard()

func _build_world() -> void:
	var world_environment := WorldEnvironment.new()
	var environment := Environment.new()
	environment.background_mode = Environment.BG_SKY
	environment.ambient_light_source = Environment.AMBIENT_SOURCE_COLOR
	environment.ambient_light_color = Color(0.72, 0.78, 0.82)
	environment.ambient_light_energy = 0.8
	world_environment.environment = environment
	add_child(world_environment)

	var sun := DirectionalLight3D.new()
	sun.rotation_degrees = Vector3(-52.0, -28.0, 0.0)
	sun.light_energy = 1.15
	add_child(sun)

	camera_3d = Camera3D.new()
	camera_3d.name = "MapCamera"
	camera_3d.position = Vector3(0.0, 27.0, 25.0)
	camera_3d.rotation_degrees = Vector3(-47.0, 0.0, 0.0)
	camera_3d.projection = Camera3D.PROJECTION_ORTHOGONAL
	camera_3d.size = 39.0
	camera_3d.current = true
	add_child(camera_3d)

	var ground := MeshInstance3D.new()
	var ground_mesh := PlaneMesh.new()
	ground_mesh.size = Vector2(52.0, 52.0)
	ground.mesh = ground_mesh
	var ground_material := StandardMaterial3D.new()
	ground_material.albedo_color = Color(0.28, 0.43, 0.31)
	ground_material.roughness = 1.0
	ground.material_override = ground_material
	add_child(ground)

	_add_box("Route nationale", Vector3(0.0, 0.035, 0.0), Vector3(1.1, 0.07, 43.0), Color(0.16, 0.18, 0.19), 22.0)
	_add_box("Route transversale", Vector3(0.0, 0.045, 0.0), Vector3(42.0, 0.07, 0.9), Color(0.19, 0.20, 0.21), -12.0)

	var city_names: Array[String] = ["NOVARIS", "MONTCLAIR", "VALDOR", "PORT-ROYAL", "BELRIVE"]
	var city_positions: Array[Vector3] = [
		Vector3(0.0, 0.0, 0.0),
		Vector3(-11.0, 0.0, 9.0),
		Vector3(11.0, 0.0, 8.0),
		Vector3(-10.0, 0.0, -10.0),
		Vector3(12.0, 0.0, -9.0)
	]

	for index in range(city_names.size()):
		var city_material := StandardMaterial3D.new()
		city_material.albedo_color = Color(0.88, 0.67, 0.25) if index == 0 else Color(0.31, 0.61, 0.77)
		var marker := MeshInstance3D.new()
		var marker_mesh := SphereMesh.new()
		marker_mesh.radius = 0.85 if index == 0 else 0.58
		marker_mesh.height = marker_mesh.radius * 2.0
		marker.mesh = marker_mesh
		marker.material_override = city_material
		marker.position = city_positions[index] + Vector3(0.0, 0.65, 0.0)
		add_child(marker)

		var city_label := Label3D.new()
		city_label.text = city_names[index]
		city_label.position = city_positions[index] + Vector3(0.0, 1.65, 0.0)
		city_label.font_size = 42
		city_label.outline_size = 8
		city_label.modulate = Color(1.0, 1.0, 1.0)
		add_child(city_label)

	for index in range(32):
		var x := sin(float(index) * 1.71) * 18.0
		var z := cos(float(index) * 1.19) * 17.0
		var height := randf_range(0.5, 2.8)
		var building_color := Color(0.47, 0.53, 0.58).lerp(Color(0.72, 0.69, 0.59), randf() * 0.65)
		_add_box("Bâtiment %02d" % index, Vector3(x, height * 0.5, z), Vector3(randf_range(0.6, 1.3), height, randf_range(0.6, 1.3)), building_color)

func _add_box(object_name: String, position_3d: Vector3, dimensions: Vector3, color: Color, rotation_y: float = 0.0) -> void:
	var item := MeshInstance3D.new()
	item.name = object_name
	var box := BoxMesh.new()
	box.size = dimensions
	item.mesh = box
	var material := StandardMaterial3D.new()
	material.albedo_color = color
	material.roughness = 0.9
	item.material_override = material
	item.position = position_3d
	item.rotation_degrees.y = rotation_y
	add_child(item)

func _build_interface() -> void:
	var layer := CanvasLayer.new()
	layer.layer = 10
	add_child(layer)

	var panel := PanelContainer.new()
	panel.position = Vector2(18.0, 18.0)
	panel.custom_minimum_size = Vector2(330.0, 250.0)
	var panel_style := StyleBoxFlat.new()
	panel_style.bg_color = Color(0.055, 0.075, 0.09, 0.94)
	panel_style.border_color = Color(0.72, 0.56, 0.25, 1.0)
	panel_style.set_border_width_all(2)
	panel_style.set_corner_radius_all(12)
	panel.add_theme_stylebox_override("panel", panel_style)
	layer.add_child(panel)

	var content := VBoxContainer.new()
	content.add_theme_constant_override("separation", 7)
	panel.add_child(content)

	var title := Label.new()
	title.text = "CITY PRESIDENT"
	title.add_theme_font_size_override("font_size", 25)
	title.add_theme_color_override("font_color", Color(0.92, 0.76, 0.39))
	content.add_child(title)

	date_label = _make_label("Année 1 — Jour 1")
	content.add_child(date_label)
	content.add_child(HSeparator.new())

	budget_label = _make_label("")
	content.add_child(budget_label)
	debt_label = _make_label("")
	content.add_child(debt_label)
	gdp_label = _make_label("")
	content.add_child(gdp_label)
	unemployment_label = _make_label("")
	content.add_child(unemployment_label)
	inflation_label = _make_label("")
	content.add_child(inflation_label)

	var speed_row := HBoxContainer.new()
	speed_row.add_theme_constant_override("separation", 6)
	content.add_child(speed_row)
	_add_speed_button(speed_row, "PAUSE", 0.0)
	_add_speed_button(speed_row, "x1", 1.0)
	_add_speed_button(speed_row, "x5", 5.0)
	_add_speed_button(speed_row, "x10", 10.0)

	var hint := Label.new()
	hint.text = "Glisse pour déplacer la carte • Pince pour zoomer"
	hint.position = Vector2(18.0, 278.0)
	hint.add_theme_color_override("font_color", Color(1.0, 1.0, 1.0))
	hint.add_theme_font_size_override("font_size", 16)
	layer.add_child(hint)

func _make_label(text_value: String) -> Label:
	var label := Label.new()
	label.text = text_value
	label.add_theme_font_size_override("font_size", 16)
	label.add_theme_color_override("font_color", Color(0.91, 0.93, 0.94))
	return label

func _add_speed_button(parent: HBoxContainer, caption: String, multiplier: float) -> void:
	var button := Button.new()
	button.text = caption
	button.custom_minimum_size = Vector2(66.0, 42.0)
	button.size_flags_horizontal = Control.SIZE_EXPAND_FILL
	button.pressed.connect(_set_speed.bind(multiplier))
	parent.add_child(button)

func _set_speed(multiplier: float) -> void:
	speed_multiplier = multiplier
	if multiplier <= 0.0:
		simulation_timer.stop()
	else:
		simulation_timer.wait_time = 8.0 / multiplier
		simulation_timer.start()

func _advance_day() -> void:
	economy.advance_day()
	game_day += 1
	if game_day > 30:
		game_day = 1
		game_month += 1
	if game_month > 12:
		game_month = 1
		game_year += 1
	_refresh_dashboard()

func _refresh_dashboard() -> void:
	if not is_instance_valid(budget_label):
		return
	date_label.text = "Novaria • Année %d • Mois %d • Jour %d" % [game_year, game_month, game_day]
	budget_label.text = "Budget national : " + economy.billions(economy.national_budget)
	debt_label.text = "Dette publique : " + economy.billions(economy.public_debt)
	gdp_label.text = "PIB : " + economy.billions(economy.gross_domestic_product)
	unemployment_label.text = "Chômage : %.1f %%" % economy.unemployment_rate
	inflation_label.text = "Inflation : %.1f %%" % economy.inflation_rate

func _input(event: InputEvent) -> void:
	if camera_3d == null:
		return
	if event is InputEventScreenTouch:
		if event.pressed:
			touch_positions[event.index] = event.position
			if touch_positions.size() == 2:
				var points: Array = touch_positions.values()
				previous_pinch_distance = points[0].distance_to(points[1])
		else:
			touch_positions.erase(event.index)
			previous_pinch_distance = 0.0
	elif event is InputEventScreenDrag:
		var old_distance := previous_pinch_distance
		touch_positions[event.index] = event.position
		if touch_positions.size() >= 2:
			var points: Array = touch_positions.values()
			var new_distance: float = points[0].distance_to(points[1])
			if old_distance > 0.0:
				_zoom_camera((old_distance - new_distance) * 0.035)
			previous_pinch_distance = new_distance
		else:
			_pan_camera(event.relative)
	elif event is InputEventMouseMotion and Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT):
		_pan_camera(event.relative)
	elif event is InputEventMouseButton and event.pressed:
		if event.button_index == MOUSE_BUTTON_WHEEL_UP:
			_zoom_camera(-2.0)
		elif event.button_index == MOUSE_BUTTON_WHEEL_DOWN:
			_zoom_camera(2.0)

func _pan_camera(delta: Vector2) -> void:
	camera_3d.position.x -= delta.x * 0.035
	camera_3d.position.z += delta.y * 0.035

func _zoom_camera(amount: float) -> void:
	camera_3d.size = clampf(camera_3d.size + amount, 18.0, 65.0)
