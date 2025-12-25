# Network Snapshot Compression

## Goal
Explore techniques for reducing bandwidth usage in multiplayer games
by sending compressed, delta-based player state snapshots instead of
full state updates every frame.

## Concepts Implemented
- Quantized position data
- Bit-level packing
- Change masks for delta updates
- Snapshot sequence IDs with wraparound-safe ordering
- Local simulation of sender and receiver
- Smooth interpolation on the receiving side

## Bandwidth Characteristics
Packet size varies because only changed fields are transmitted.
Small movements typically cost 29–46 bits, while larger movements
such as cell transitions cost 87 bits.

## Why This Matters
Efficient snapshot compression is critical for real-time multiplayer games
to reduce bandwidth usage while maintaining smooth and responsive movement,
especially under variable network conditions.

## Notes
This module is a self-driven learning exercise inspired by common
multiplayer networking problems. It does not contain or reference
any proprietary or confidential material.
