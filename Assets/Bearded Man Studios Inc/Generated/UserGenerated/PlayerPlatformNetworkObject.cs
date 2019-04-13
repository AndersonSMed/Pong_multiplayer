using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0,0,0]")]
	public partial class PlayerPlatformNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector2 _position;
		public event FieldEvent<Vector2> positionChanged;
		public InterpolateVector2 positionInterpolation = new InterpolateVector2() { LerpT = 0.15f, Enabled = true };
		public Vector2 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private bool _alive;
		public event FieldEvent<bool> aliveChanged;
		public Interpolated<bool> aliveInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool alive
		{
			get { return _alive; }
			set
			{
				// Don't do anything if the value is the same
				if (_alive == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_alive = value;
				hasDirtyFields = true;
			}
		}

		public void SetaliveDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_alive(ulong timestep)
		{
			if (aliveChanged != null) aliveChanged(_alive, timestep);
			if (fieldAltered != null) fieldAltered("alive", _alive, timestep);
		}
		[ForgeGeneratedField]
		private uint _player;
		public event FieldEvent<uint> playerChanged;
		public Interpolated<uint> playerInterpolation = new Interpolated<uint>() { LerpT = 0f, Enabled = false };
		public uint player
		{
			get { return _player; }
			set
			{
				// Don't do anything if the value is the same
				if (_player == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_player = value;
				hasDirtyFields = true;
			}
		}

		public void SetplayerDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_player(ulong timestep)
		{
			if (playerChanged != null) playerChanged(_player, timestep);
			if (fieldAltered != null) fieldAltered("player", _player, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _direction;
		public event FieldEvent<Vector3> directionChanged;
		public InterpolateVector3 directionInterpolation = new InterpolateVector3() { LerpT = 0f, Enabled = false };
		public Vector3 direction
		{
			get { return _direction; }
			set
			{
				// Don't do anything if the value is the same
				if (_direction == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_direction = value;
				hasDirtyFields = true;
			}
		}

		public void SetdirectionDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_direction(ulong timestep)
		{
			if (directionChanged != null) directionChanged(_direction, timestep);
			if (fieldAltered != null) fieldAltered("direction", _direction, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			aliveInterpolation.current = aliveInterpolation.target;
			playerInterpolation.current = playerInterpolation.target;
			directionInterpolation.current = directionInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _alive);
			UnityObjectMapper.Instance.MapBytes(data, _player);
			UnityObjectMapper.Instance.MapBytes(data, _direction);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector2>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_alive = UnityObjectMapper.Instance.Map<bool>(payload);
			aliveInterpolation.current = _alive;
			aliveInterpolation.target = _alive;
			RunChange_alive(timestep);
			_player = UnityObjectMapper.Instance.Map<uint>(payload);
			playerInterpolation.current = _player;
			playerInterpolation.target = _player;
			RunChange_player(timestep);
			_direction = UnityObjectMapper.Instance.Map<Vector3>(payload);
			directionInterpolation.current = _direction;
			directionInterpolation.target = _direction;
			RunChange_direction(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _alive);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _player);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _direction);

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
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector2>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector2>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (aliveInterpolation.Enabled)
				{
					aliveInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					aliveInterpolation.Timestep = timestep;
				}
				else
				{
					_alive = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_alive(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (playerInterpolation.Enabled)
				{
					playerInterpolation.target = UnityObjectMapper.Instance.Map<uint>(data);
					playerInterpolation.Timestep = timestep;
				}
				else
				{
					_player = UnityObjectMapper.Instance.Map<uint>(data);
					RunChange_player(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (directionInterpolation.Enabled)
				{
					directionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					directionInterpolation.Timestep = timestep;
				}
				else
				{
					_direction = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_direction(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector2)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (aliveInterpolation.Enabled && !aliveInterpolation.current.UnityNear(aliveInterpolation.target, 0.0015f))
			{
				_alive = (bool)aliveInterpolation.Interpolate();
				//RunChange_alive(aliveInterpolation.Timestep);
			}
			if (playerInterpolation.Enabled && !playerInterpolation.current.UnityNear(playerInterpolation.target, 0.0015f))
			{
				_player = (uint)playerInterpolation.Interpolate();
				//RunChange_player(playerInterpolation.Timestep);
			}
			if (directionInterpolation.Enabled && !directionInterpolation.current.UnityNear(directionInterpolation.target, 0.0015f))
			{
				_direction = (Vector3)directionInterpolation.Interpolate();
				//RunChange_direction(directionInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerPlatformNetworkObject() : base() { Initialize(); }
		public PlayerPlatformNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerPlatformNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
