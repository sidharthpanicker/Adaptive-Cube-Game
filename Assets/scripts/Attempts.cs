using SQLite4Unity3d;

public class Attempts  {

	[PrimaryKey, AutoIncrement]
	public int AttemptId { get; set; }
	public int UserID { get; set; }
	public float Time { get; set; }
	public int SessionId { get; set; }
	public int Level { get; set; }
	public float SideForce { get; set; }
	public float Speed { get; set; }
	public string FailureType { get; set; }
	public bool Completed { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Session: SessionId={0}, UserID={1}]", SessionId, UserID);
	}
}