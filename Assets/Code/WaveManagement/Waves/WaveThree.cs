using Assets.Code.WaveManagement;

public class WaveThree : Wave
{
    public override int ZOMBIECOUNT => 150;
    public override int SEQUENCE => 3;
    public override int MAXZOMBIECOUNT => 16;
    public override bool NEXTTRACK => true;
}
