﻿using System;
using System.Collections.Generic;
using System.Linq;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.CharacterScripts
{
    /// <summary>
    /// Character presentation in editor. Contains sprites, renderers, animation and so on.
    /// </summary>
    public partial class Character : CharacterBase
    {
		//[Header("Weapons")]
		//public MeleeWeapon MeleeWeapon;
		//public BowShooting BowShooting;

	    [Header("Anchors")]
	    public Transform AnchorBody;
	    public Transform AnchorSword;
	    public Transform AnchorBow;

		[Header("Service")]
		public LayerManager LayerManager;

        [Header("Custom")]
        public List<Sprite> Underwear;
        public Color UnderwearColor;
		
		/// <summary>
		/// Called automatically when something was changed.
		/// </summary>
		public void OnValidate()
	    {
            //Initialize();
        }
        
        /// <summary>
        /// Called automatically when object was enabled.
        /// </summary>
		public void OnEnable()
		{
			UpdateAnimation();
		}

	    //public void OnDisable()
	    //{
		//    _animationState = -1;
	    //}

	    //private int _animationState = -1;

		/// <summary>
		/// Refer to Animator window to learn animation params, states and transitions!
		/// </summary>
		public override void UpdateAnimation()
		{
			//if (!Animator.isInitialized) return;

			//var state = 100 * (int) WeaponType;

			//Animator.SetInteger("WeaponType", (int) WeaponType);

			//if (state == _animationState) return; // No need to change animation.

			//_animationState = state;
			//Animator.SetBool("Ready", true);
			//Animator.SetBool("Stand", true);
			//Animator.Play("IdleMelee", 0); // Upper body
			//Animator.Play("Stand", 1); // Lower body
		}

        /// <summary>
        /// Initializes character renderers with selected sprites.
        /// </summary>
        public override void Initialize()
        {
            try // Disable try/catch for debugging.
            {
                TryInitialize();
            }
            catch (Exception e)
            {
                Debug.LogWarningFormat("Unable to initialize character {0}: {1}", name, e.Message);
            }
        }

		/// <summary>
		/// Set character's expression.
		/// </summary>
	    public void SetExpression(string expression)
		{
			//throw new NotImplementedException();
			if (Expressions.Count < 3) throw new Exception("Character must have at least 3 basic expressions: Default, Angry and Dead.");
			if (EyesRenderer == null) return;
			
			var e = Expressions.Single(i => i.Name == expression);

			Expression = expression;
			EyebrowsRenderer.sprite = e.Eyebrows;
			EyesRenderer.sprite = e.Eyes;
			MouthRenderer.sprite = e.Mouth;

			//if (EyebrowsRenderer.sprite == null) EyebrowsRenderer.sprite = Expressions[0].Eyebrows;
			//if (EyesRenderer.sprite == null) EyesRenderer.sprite = Expressions[0].Eyes;
			//if (MouthRenderer.sprite == null) MouthRenderer.sprite = Expressions[0].Mouth;
		}

	    /// <summary>
	    /// Set character's body.
	    /// </summary>
	    public void SetBody(List<Sprite> body)
	    {
		    Body = body;
			Initialize();
		}

		#region Equipment

		/// <summary>
		/// Remove all equipment.
		/// </summary>
		public override void ResetEquipment()
	    {
            Armor = new List<Sprite>();
			CompositeWeapon = new List<Sprite>();
            Shield = new List<Sprite>();
			Helmet = Cape = Back = PrimaryWeapon = SecondaryWeapon = null;
            HideEars = false;
			Initialize();
	    }

		/// <summary>
		/// Equip melee weapon.
		/// </summary>
		/// <param name="sprite">Weapon sprite. It can be obtained from SpriteCollection.Instance.MeleeWeapon1H/2H[].Sprites.</param>
		/// <param name="twoHanded">If two-handed melee weapon.</param>
		public void EquipMeleeWeapon(Sprite sprite, bool twoHanded = false)
	    {
		    PrimaryWeapon = sprite;
            CompositeWeapon = null;
            if (twoHanded) Shield = null;
			WeaponType = twoHanded ? WeaponType.Melee2H : WeaponType.Melee1H;
		    Initialize();
	    }
	   
	    /// <summary>
	    /// Equip paired melee weapons.
	    /// </summary>
	    public void EquipMeleeWeaponPaired(Sprite spritePrimary, Sprite spriteSecondary)
	    {
			throw new NotImplementedException();
            //PrimaryMeleeWeapon = spritePrimary;
            //SecondaryMeleeWeapon = spriteSecondary;
            //CompositeWeapon = null;
            //WeaponType = WeaponType.MeleePaired;
            //Initialize();
		}

		/// <summary>
		/// Equip bow.
		/// </summary>
		/// <param name="sprites">A list of sprites from bow atlas (multiple sprite). It can be obtained from SpriteCollection.Instance.Bow[].Sprites.</param>
		public void EquipBow(List<Sprite> sprites)
        {
            Shield = new List<Sprite>();
			PrimaryWeapon = SecondaryWeapon = null;
			CompositeWeapon = sprites;
		    WeaponType = WeaponType.Bow;
			Initialize();
	    }

		/// <summary>
		/// Equip shield.
		/// </summary>
		/// <param name="sprite">Shield sprite. It can be obtained from SpriteCollection.Instance.Shield[].Sprite.</param>
		public void EquipShield(List<Sprite> sprites)
	    {
		    Shield = sprites;
            CompositeWeapon = null;
            if (WeaponType != WeaponType.Melee1H) PrimaryWeapon = SecondaryWeapon = null;
			WeaponType = WeaponType.Melee1H;
		    Initialize();
	    }

		/// <summary>
		/// Equip helmet.
		/// </summary>
		/// <param name="sprite">Helmet sprite. It can be obtained from SpriteCollection.Instance.Helmet[].Sprite.</param>
		public void EquipHelmet(Sprite sprite)
	    {
			// TODO: You can set HideEars and CropHair properties here based on some external data. Don't use texture.name as it may be packed.
			Helmet = sprite;
		    Initialize();
	    }

		/// <summary>
		/// Equip armor.
		/// </summary>
		/// <param name="sprites">A list of sprites from armor atlas (multiple sprite). It can be obtained from SpriteCollection.Instance.Armor[].Sprites.</param>
		public void EquipArmor(List<Sprite> sprites)
	    {
		    Armor = sprites;
		    Initialize();
	    }

		/// <summary>
		/// Equip armor.
		/// </summary>
		/// <param name="sprites">A list of sprites from armor atlas (multiple sprite). It can be obtained from SpriteCollection.Instance.Armor[].Sprites.</param>
		public void EquipUpperArmor(List<Sprite> sprites)
	    {
			foreach (var part in new[] { "ArmL", "ArmR", "Finger", "ForearmL", "ForearmR", "HandL", "HandR", "SleeveR", "Torso" })
			{
				SetArmorPart(part, sprites);
			}

			Initialize();
	    }

		/// <summary>
		/// Equip lower armor.
		/// </summary>
		/// <param name="sprites">A list of sprites from armor atlas (multiple sprite). It can be obtained from SpriteCollection.Instance.Armor[].Sprites.</param>
		public void EquipLowerArmor(List<Sprite> sprites)
	    {
		    foreach (var part in new[] { "Leg", "Pelvis", "Shin" })
		    {
			    SetArmorPart(part, sprites);
		    }

		    Initialize();
	    }

		/// <summary>
		/// Equip gloves.
		/// </summary>
		/// <param name="sprites">A list of sprites from armor atlas (multiple sprite). It can be obtained from SpriteCollection.Instance.Armor[].Sprites.</param>
		public void EquipGloves(List<Sprite> sprites)
	    {
		    foreach (var part in new[] { "HandL", "HandR", "Finger" })
		    {
			    SetArmorPart(part, sprites);
		    }

		    Initialize();
	    }

		/// <summary>
		/// Equip boots.
		/// </summary>
		/// <param name="sprites">A list of sprites from armor atlas (multiple sprite). It can be obtained from SpriteCollection.Instance.Armor[].Sprites.</param>
		public void EquipBoots(List<Sprite> sprites)
	    {
		    foreach (var part in new[] { "Shin" })
		    {
			    SetArmorPart(part, sprites);
		    }

		    Initialize();
	    }

		private void SetArmorPart(string part, List<Sprite> armor)
	    {
		    var sprite = armor.Single(j => j.name == part);

		    Armor.RemoveAll(i => i == null);

		    for (var i = 0; i < Armor.Count; i++)
		    {
			    if (Armor[i] != null && Armor[i].name == part)
			    {
				    Armor[i] = sprite;
				    return;
			    }
		    }

		    Armor.Add(sprite);
	    }

		#endregion

	    /// <summary>
		/// Initializes character renderers with selected sprites.
		/// </summary>
		private void TryInitialize()
        {
			//if (Expressions.All(i => i.Name != "Default") || Expressions.All(i => i.Name != "Angry") || Expressions.All(i => i.Name != "Dead"))
			//	throw new Exception("Character must have at least 3 basic expressions: Default, Angry and Dead.");

			HairRenderer.sprite = Hair == null ? null : CropHair ? HairCropped : Hair;
			MapSprites(EarsRenderers, Ears);
            EarsRenderers.ForEach(i => i.enabled = !HideEars);
            SetExpression(Expression);
			if (BeardRenderer != null) BeardRenderer.sprite = Beard;
			MapSprites(BodyRenderers, Body);
			HelmetRenderer.sprite = Helmet;
            MapSprites(ArmorRenderers, Armor);
			//CapeRenderer.sprite = Cape;
			//BackRenderer.sprite = Back;
	        PrimaryWeaponRenderer.sprite = PrimaryWeapon;
            PrimaryWeaponRenderer.enabled = WeaponType == WeaponType.Melee1H || WeaponType == WeaponType.Melee2H || WeaponType == WeaponType.Paired || WeaponType == WeaponType.Crossbow;
			SecondaryWeaponRenderer.sprite = SecondaryWeapon;
            SecondaryWeaponRenderer.enabled = WeaponType == WeaponType.Paired;
			MapSprites(BowRenderers, CompositeWeapon);
            BowRenderers.ForEach(i => i.enabled = WeaponType == WeaponType.Bow);
			//FirearmsRenderers.ForEach(i => i.sprite = Firearms.SingleOrDefault(j => j != null && i.name.Contains(j.name)));
			MapSprites(ShieldRenderers, Shield);
            ShieldRenderers.ForEach(i => i.enabled = WeaponType == WeaponType.Melee1H);
			
			if (MakeupRenderer != null) MakeupRenderer.sprite = Makeup;
            if (MaskRenderer != null) MaskRenderer.sprite = Mask;
			MapSprites(EarringsRenderers, Earrings);

			if (WeaponType == WeaponType.Crossbow)
            {
                var quiver = BowRenderers.Single(i => i.name == "Quiver");

                quiver.enabled = true;
				MapSprites(new List<SpriteRenderer> { PrimaryWeaponRenderer, quiver }, CompositeWeapon);
			}
		}

		private void MapSprites(List<SpriteRenderer> spriteRenderers, List<Sprite> sprites)
        {
            foreach (var part in spriteRenderers)
            {
                part.sprite = sprites == null ? null : part.GetComponent<SpriteMapping>().FindSprite(sprites);
            }
        }
    }
}