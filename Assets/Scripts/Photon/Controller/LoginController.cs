using ARPGCommon;
using ExitGames.Client.Photon;


public class LoginController : ControllerBase
{
    // Use this for initialization
    private void Start()
    {
        PhotonEngine.Instance.RegisterController(OperationCode.Login, this);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        throw new System.NotImplementedException();
    }
}