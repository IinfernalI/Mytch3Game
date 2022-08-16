using GameStateMashine;
using Services;

namespace Controller
{
    public class GameStuffHolder
    {
        private GameFactory gameFactory;
        private AssetProvider _assetProvider;

        public GameStuffHolder()
        {
            _assetProvider = new AssetProvider();
            gameFactory = new GameFactory(_assetProvider);
            GameStateMachine = new GameStateMachine(this, gameFactory);
        }

        public GameStateMachine GameStateMachine { get; set; }
   
    }
}