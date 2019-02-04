using SQLite4Unity3d;

public class Session  {

	[PrimaryKey, AutoIncrement]
	public int SessionId { get; set; }
	public int UserID { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Session: SessionId={0}, UserID={1}]", SessionId, UserID);
	}
}