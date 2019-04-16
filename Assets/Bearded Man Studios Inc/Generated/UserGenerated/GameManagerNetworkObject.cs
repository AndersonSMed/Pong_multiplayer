using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0,0,0]")]
	public partial class GameManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private bool _GameStarted;
		public event FieldEvent<bool> GameStartedChanged;
		public Interpolated<bool> GameStartedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool GameStarted
		{
			get { return _GameStarted; }
			set
			{
				// Don't do anything if the value is the same
				if (_GameStarted == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_GameStarted = value;
				hasDirtyFields = true;
			}
		}

		public void SetGameStartedDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_GameStarted(ulong timestep)
		{
			if (GameStartedChanged != null) GameStartedChanged(_GameStarted, timestep);
			if (fieldAltered != null) fieldAltered("GameStarted", _GameStarted, timestep);
		}
		[ForgeGeneratedField]
		private int _LoggedPlayers;
		public event FieldEvent<int> LoggedPlayersChanged;
		public Interpolated<int> LoggedPlayersInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int LoggedPlayers
		{
			get { return _LoggedPlayers; }
			set
			{
				// Don't do anything if the value is the same
				if (_LoggedPlayers == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_LoggedPlayers = value;
				hasDirtyFields = true;
			}
		}

		public void SetLoggedPlayersDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_LoggedPlayers(ulong timestep)
		{
			if (LoggedPlayersChanged != null) LoggedPlayersChanged(_LoggedPlayers, timestep);
			if (fieldAltered != null) fieldAltered("LoggedPlayers", _LoggedPlayers, timestep);
		}
		[ForgeGeneratedField]
		private bool _WaitingPlayers;
		public event FieldEvent<bool> WaitingPlayersChanged;
		public Interpolated<bool> WaitingPlayersInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool WaitingPlayers
		{
			get { return _WaitingPlayers; }
			set
			{
				// Don't do anything if the value is the same
				if (_WaitingPlayers == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_WaitingPlayers = value;
				hasDirtyFields = true;
			}
		}

		public void SetWaitingPlayersDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_WaitingPlayers(ulong timestep)
		{
			if (WaitingPlayersChanged != null) WaitingPlayersChanged(_WaitingPlayers, timestep);
			if (fieldAltered != null) fieldAltered("WaitingPlayers", _WaitingPlayers, timestep);
		}
		[ForgeGeneratedField]
		private bool _GameEnded;
		public event FieldEvent<bool> GameEndedChanged;
		public Interpolated<bool> GameEndedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool GameEnded
		{
			get { return _GameEnded; }
			set
			{
				// Don't do anything if the value is the same
				if (_GameEnded == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_GameEnded = value;
				hasDirtyFields = true;
			}
		}

		public void SetGameEndedDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_GameEnded(ulong timestep)
		{
			if (GameEndedChanged != null) GameEndedChanged(_GameEnded, timestep);
			if (fieldAltered != null) fieldAltered("GameEnded", _GameEnded, timestep);
		}
		[ForgeGeneratedField]
		private uint _Winner;
		public event FieldEvent<uint> WinnerChanged;
		public Interpolated<uint> WinnerInterpolation = new Interpolated<uint>() { LerpT = 0f, Enabled = false };
		public uint Winner
		{
			get { return _Winner; }
			set
			{
				// Don't do anything if the value is the same
				if (_Winner == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_Winner = value;
				hasDirtyFields = true;
			}
		}

		public void SetWinnerDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_Winner(ulong timestep)
		{
			if (WinnerChanged != null) WinnerChanged(_Winner, timestep);
			if (fieldAltered != null) fieldAltered("Winner", _Winner, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			GameStartedInterpolation.current = GameStartedInterpolation.target;
			LoggedPlayersInterpolation.current = LoggedPlayersInterpolation.target;
			WaitingPlayersInterpolation.current = WaitingPlayersInterpolation.target;
			GameEndedInterpolation.current = GameEndedInterpolation.target;
			WinnerInterpolation.current = WinnerInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _GameStarted);
			UnityObjectMapper.Instance.MapBytes(data, _LoggedPlayers);
			UnityObjectMapper.Instance.MapBytes(data, _WaitingPlayers);
			UnityObjectMapper.Instance.MapBytes(data, _GameEnded);
			UnityObjectMapper.Instance.MapBytes(data, _Winner);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_GameStarted = UnityObjectMapper.Instance.Map<bool>(payload);
			GameStartedInterpolation.current = _GameStarted;
			GameStartedInterpolation.target = _GameStarted;
			RunChange_GameStarted(timestep);
			_LoggedPlayers = UnityObjectMapper.Instance.Map<int>(payload);
			LoggedPlayersInterpolation.current = _LoggedPlayers;
			LoggedPlayersInterpolation.target = _LoggedPlayers;
			RunChange_LoggedPlayers(timestep);
			_WaitingPlayers = UnityObjectMapper.Instance.Map<bool>(payload);
			WaitingPlayersInterpolation.current = _WaitingPlayers;
			WaitingPlayersInterpolation.target = _WaitingPlayers;
			RunChange_WaitingPlayers(timestep);
			_GameEnded = UnityObjectMapper.Instance.Map<bool>(payload);
			GameEndedInterpolation.current = _GameEnded;
			GameEndedInterpolation.target = _GameEnded;
			RunChange_GameEnded(timestep);
			_Winner = UnityObjectMapper.Instance.Map<uint>(payload);
			WinnerInterpolation.current = _Winner;
			WinnerInterpolation.target = _Winner;
			RunChange_Winner(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _GameStarted);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _LoggedPlayers);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _WaitingPlayers);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _GameEnded);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Winner);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (GameStartedInterpolation.Enabled)
				{
					GameStartedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					GameStartedInterpolation.Timestep = timestep;
				}
				else
				{
					_GameStarted = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_GameStarted(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (LoggedPlayersInterpolation.Enabled)
				{
					LoggedPlayersInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					LoggedPlayersInterpolation.Timestep = timestep;
				}
				else
				{
					_LoggedPlayers = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_LoggedPlayers(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (WaitingPlayersInterpolation.Enabled)
				{
					WaitingPlayersInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					WaitingPlayersInterpolation.Timestep = timestep;
				}
				else
				{
					_WaitingPlayers = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_WaitingPlayers(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (GameEndedInterpolation.Enabled)
				{
					GameEndedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					GameEndedInterpolation.Timestep = timestep;
				}
				else
				{
					_GameEnded = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_GameEnded(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (WinnerInterpolation.Enabled)
				{
					WinnerInterpolation.target = UnityObjectMapper.Instance.Map<uint>(data);
					WinnerInterpolation.Timestep = timestep;
				}
				else
				{
					_Winner = UnityObjectMapper.Instance.Map<uint>(data);
					RunChange_Winner(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (GameStartedInterpolation.Enabled && !GameStartedInterpolation.current.UnityNear(GameStartedInterpolation.target, 0.0015f))
			{
				_GameStarted = (bool)GameStartedInterpolation.Interpolate();
				//RunChange_GameStarted(GameStartedInterpolation.Timestep);
			}
			if (LoggedPlayersInterpolation.Enabled && !LoggedPlayersInterpolation.current.UnityNear(LoggedPlayersInterpolation.target, 0.0015f))
			{
				_LoggedPlayers = (int)LoggedPlayersInterpolation.Interpolate();
				//RunChange_LoggedPlayers(LoggedPlayersInterpolation.Timestep);
			}
			if (WaitingPlayersInterpolation.Enabled && !WaitingPlayersInterpolation.current.UnityNear(WaitingPlayersInterpolation.target, 0.0015f))
			{
				_WaitingPlayers = (bool)WaitingPlayersInterpolation.Interpolate();
				//RunChange_WaitingPlayers(WaitingPlayersInterpolation.Timestep);
			}
			if (GameEndedInterpolation.Enabled && !GameEndedInterpolation.current.UnityNear(GameEndedInterpolation.target, 0.0015f))
			{
				_GameEnded = (bool)GameEndedInterpolation.Interpolate();
				//RunChange_GameEnded(GameEndedInterpolation.Timestep);
			}
			if (WinnerInterpolation.Enabled && !WinnerInterpolation.current.UnityNear(WinnerInterpolation.target, 0.0015f))
			{
				_Winner = (uint)WinnerInterpolation.Interpolate();
				//RunChange_Winner(WinnerInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public GameManagerNetworkObject() : base() { Initialize(); }
		public GameManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public GameManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
