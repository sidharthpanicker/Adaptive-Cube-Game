using SQLite4Unity3d;

public class Cordinates  {

	[PrimaryKey, AutoIncrement]
	public int CordinatesId { get; set; }
	public float X_Cord { get; set; }
	public float Y_Yord { get; set; }
	public float Z_Cord { get; set; }
	public float Time { get; set; }
	public int SessionId { get; set; }
	public int Level { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Cordinates: CordinatesId={0}, X_Cord={1},  Y_Yord={2}, Z_Cord={3}, Time={4},  SessionId={5}, Level={6}]", CordinatesId, X_Cord,  Y_Yord, Z_Cord, Time,  SessionId, Level);
	}
}