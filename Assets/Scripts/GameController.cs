using ServiceLocator;
using UnityEngine;

public class GameController : MonoBehaviour, IService
{
        public bool GameStop { get; private set; }

        public void Stop()
        {
                GameStop = true;
        }

        public void Play()
        {
                GameStop = false;
        }
}