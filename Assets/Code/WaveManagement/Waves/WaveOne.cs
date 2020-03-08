using Assets.Code.WaveManagement;

public class WaveOne : Wave
{
    public override int ZOMBIECOUNT => 20;
    public override int SEQUENCE => 1;
    public override int MAXZOMBIECOUNT => 5;
    public override bool NEXTTRACK => true;
}

