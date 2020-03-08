using Assets.Code.WaveManagement;

public class WaveTwo : Wave
{
    public override int ZOMBIECOUNT => 40;
    public override int SEQUENCE => 2;
    public override int MAXZOMBIECOUNT => 15;
    public override bool NEXTTRACK => false;
}
