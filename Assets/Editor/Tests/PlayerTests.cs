using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests {
	[Test]
	public void PlayerTests_TakeDamage_GreaterZero() {
		Player player = new Player();
		int damage = 10;		
		player.TakeDamage(damage);
		Assert.AreEqual(player.Hp, player.maxHp - damage);
	}

	[Test]
	public void PlayerTests_TakeDamage_Dead() {
		Player player = new Player();
		int damage = player.maxHp;
		player.TakeDamage(damage);
		Assert.IsTrue(player.IsDead);

		player.TakeDamage(damage);
		Assert.AreEqual(player.Hp, 0);
		Assert.IsTrue(player.IsDead);
	}	
}