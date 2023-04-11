using Raylib_cs;

namespace main {
    
    public class Player {

        public string name;
        public Color color;
        public int lives;
        public int score = 0;
        public List<KeyboardKey> controls;
        public Random rnd = new Random();
        public Unit? unit;
        public bool popup_1 = false;
        
        // constructor
        public Player(string name_p,
                      Color color_p,
                      List<KeyboardKey> controls_p,
                      int lives_p) {
            name = name_p;
            color = color_p;
            controls = controls_p;
            lives = lives_p;
        }

        public void events(float dt_factor, Scene active_scene) {
            if (Raylib.IsKeyDown(controls[0]) ||
                Raylib.IsKeyDown(controls[1]) ||
                Raylib.IsKeyDown(controls[2]) || 
                Raylib.IsKeyDown(controls[3])) {
                if (unit != null) {
                    if (Raylib.IsKeyDown(controls[0])) {unit.move_left(dt_factor, active_scene);}
                    if (Raylib.IsKeyDown(controls[1])) {unit.move_up(dt_factor, active_scene);}
                    if (Raylib.IsKeyDown(controls[2])) {unit.move_right(dt_factor, active_scene);}
                    if (Raylib.IsKeyDown(controls[3])) {unit.move_down(dt_factor, active_scene);}
                }
            }
            else {unit.stop();}
            
            if (Raylib.IsKeyPressed(controls[4])) {
                if (!unit.is_dead) {
                    unit.shoot(active_scene.player_bullets);
                }
                else {
                    if (lives > 0) {
                        if (active_scene.respawn_point_x != 0 && active_scene.respawn_point_y != 0) {
                            if (unit.respawn_at(active_scene.respawn_point_x, active_scene.respawn_point_y) == true) {
                                lives -= 1;
                            }
                        }
                        else if (unit.respawn_at(unit.pos_x, unit.pos_y) == true) {
                            lives -= 1;
                        }
                    }
                }
            }

            if (Raylib.IsKeyPressed(controls[5])) {unit.next_weapon();}

            if (Raylib.IsKeyPressed(controls[6])) {
                foreach (Item i in unit.items) {
                    if (i.name.Equals("map")) {
                        popup_1 = !popup_1; // swamp map
                    }
                }
            }
        }

        public void update(float dt, float dt_factor, Scene active_scene) {
            unit.update(dt, dt_factor, active_scene.player_bullets);
        }

        public void draw() {}
    }
}