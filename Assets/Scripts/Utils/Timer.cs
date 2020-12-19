namespace SecretSantaGameJam2020.Utils {
    public class Timer {
        float _interval = float.MaxValue;
        float _completeTime;
        
        public void Init(float interval, float completeTime = 0f) {
            _completeTime = completeTime;
            _interval     = interval;
        }

        public bool Tick(float passedTime) {
            _completeTime += passedTime;
            if ( _completeTime > _interval ) {
                _completeTime -= _interval;
                return true;
            }
            return false;
        }

        public void Stop() {
            _interval     = float.MaxValue;
            _completeTime = 0f;
        }
    }
}