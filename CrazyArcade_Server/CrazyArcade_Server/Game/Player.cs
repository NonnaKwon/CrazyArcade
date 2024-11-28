using System;
using System.Collections.Generic;
using System.Linq;
using static Define;

namespace CrazyArcade_Server.Game
{
    public class Player
    {
        public string Nickname { get { return _nickname; } }
        public bool IsReady { get { return _isReady; } }
        public Character Character { get { return _character; } }
        private int _id;
        private string _nickname;
        private bool _isReady;
        private Character _character;

        public Player(int id)
        {
            _id = id;
            _nickname = $"Guest{id}";
            Init();
        }

        private void Init()
        {
            _isReady = false;
            _character = Character.Bazzi;
        }
    }
}
