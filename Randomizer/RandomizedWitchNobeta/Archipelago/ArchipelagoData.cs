using System.Collections.Generic;
using System.Linq;

namespace RandomizedWitchNobeta.Archipelago;

public static class ArchipelagoData
{
    public static SortedDictionary<string, string> Locations = new()
    {
        { "Shrine - 6. Broken Cross Spear from first ranged Enemy", "Lore" },
        { "Shrine - 1. Crafted Soul Reader from pot in side alcove", "Lore" },
        { "Shrine - Chest at first magic switch barrier", "Chest" },
        { "Shrine - Chest on platform at first magic switch barrier", "Chest" },
        { "Shrine - First magic switch", "Barrier" },
        { "Shrine - 3. Copper Coin in Grand Hall statue barrel", "Lore" },
        { "Shrine - Second magic switch", "Barrier" },
        { "Shrine - Meet Cat barrier", "Barrier" },
        { "Shrine - Chest behind gate", "Chest" },
        { "Shrine - 12. Pointy Witch's Hat in pot at chest behind gate", "Lore" },
        { "Shrine - 7. Deformed Cavalier Armor from Enemy before Central Hall", "Lore" },
        { "Shrine - Specter Armor", "Bosses" },
        { "Shrine - Secret passage magic switch", "Barrier" },
        { "Shrine - Underground shortcut gate switch", "Metal Gate" },
        { "Shrine - 14. Corpse Shroud from pot in Underground shortcut", "Lore" },
        { "Secret Passage - 35. Dwarven Metalwork in pot after first drop", "Lore" },
        { "Secret Passage - 36. High Elf's Mana Ring in pot beside destructible wall", "Lore" },
        { "Secret Passage - 11. Broken Queen Doll from enemy behind destructible wall", "Lore" },
        { "Secret Passage - Absorption spell chest behind destructible wall", "Chest" },
        { "Secret Passage - First fire barrier magic switch", "Barrier" },
        { "Secret Passage - 37. Forest Elf's Vest from enemy before hole in floor", "Lore" },
        { "Secret Passage - Wind spell chest in alcove during fall", "Chest" },
        { "Secret Passage - Ice spell chest behind breakable wall after fall", "Chest" },
        { "Secret Passage - 4. Unknown House Banner from big enemy at spiral stairs", "Lore" },
        { "Secret Passage - Secret area shortcut gate switch", "Metal Gate" },
        { "Secret Passage - Fire spell chest at second fire barrier magic switch", "Chest" },
        { "Secret Passage - Second fire barrier magic switch", "Barrier" },
        { "Secret Passage - 38. Dark Elf's Ear Sample from pot at magic barrier", "Lore" },
        { "Secret Passage - 19. Knight's Halberd from pot before boss", "Lore" },
        { "Secret Passage - Enraged Armor", "Bosses" },
        { "Secret Passage - 56. Knight Kingdom Crown from Enraged Armor", "Lore" },
        { "Secret Passage - Teleport from Enraged Armor", "Teleport" },
        { "Secret Passage - Defeat Enraged Armor barrier", "Barrier" },
        { "Secret Passage - Thunder spell chest after boss", "Chest" },
        { "Secret Passage - Boss shortcut gate switch", "Metal Gate" },
        { "Secret Passage - 13. Sleeve Dagger", "Lore" },
        { "Secret Passage - Dark Tunnel shortcut gate switch", "Metal Gate" },
        { "Underground - 8. Hero's Cross Sword", "Lore" },
        { "Underground - 9. Giant Axe", "Lore" },
        { "Underground - 10. Shield of the Church", "Lore" },
        { "Underground - 97. Faithful Soul Shard", "Lore" },
        { "Underground - Wind spell chest", "Chest" },
        { "Underground - 18. Soul Doll Remnant from first doll enemy", "Lore" },
        { "Underground - Chest in alcove before falling rocks", "Chest" },
        { "Underground - Chest across staircase on backtrack path", "Chest" },
        { "Underground - 2. Stained Ribbon down on glowing rock", "Lore" },
        { "Underground - Arcane chest at bridge jumping puzzle", "Chest" },
        { "Underground - Ice spell chest in bottom pit", "Chest" },
        { "Underground - 17. Tattered Maid Outfit from maid enemy", "Lore" },
        { "Underground - Cat absorption hint & gift", "Item" },
        { "Underground - Magic barrier switches at maid enemy", "Barrier" },
        { "Underground - Lava ruins shortcut gate switch", "Metal Gate" },
        { "Underground - 15. Headcutter Circular Saw", "Lore" },
        { "Underground - Tania shortcut switch on statue side", "Metal Gate" },
        { "Underground - 16. Test Subject Manacle from barrel after fire", "Lore" },
        { "Underground - Chest after fire", "Chest" },
        { "Underground - After fire magic switch", "Barrier" },
        { "Underground - Tania", "Bosses" },
        { "Underground - Tania shortcut switch on Tania side", "Metal Gate" },
        { "Underground - 98. Lost Maiden's Soul Shard from Tania", "Lore" },
        { "Lava Ruins - 26. Teddy Bear", "Lore" },
        { "Lava Ruins - 30. Merchant's Ledger from enemy", "Lore" },
        { "Lava Ruins - Chest on scaffolding", "Chest" },
        { "Lava Ruins - Chest on left side lava ledge", "Chest" },
        { "Lava Ruins - 29. Slave Branding Iron in barrel after dropping down", "Lore" },
        { "Lava Ruins - Absorption spell chest after dropping down", "Chest" },
        { "Lava Ruins - Chest on right ruins ledge at shotgun enemies magic switch", "Chest" },
        { "Lava Ruins - Magic platform switch at shotgun enemies", "Barrier" },
        { "Lava Ruins - 27. Fractured Stone Axe from red enemy behind destructible wall", "Lore" },
        { "Lava Ruins - Wind spell chest behind destructible walls", "Chest" },
        { "Lava Ruins - 20. Weathered Cloak in corner on ruins top", "Lore" },
        { "Lava Ruins - 28. Cage on path to starting area shortcut gate", "Lore" },
        { "Lava Ruins - Fake floor shortcut gate switch", "Metal Gate" },
        { "Lava Ruins - Fake floor bait item", "Item" },
        { "Lava Ruins - 23. Cursed Turquoise Necklace from scissor enemy", "Lore" },
        { "Lava Ruins - Defeat scissor enemy barrier", "Barrier" },
        { "Lava Ruins - Lift magic switch at scissor enemy", "Barrier" },
        { "Lava Ruins - 32. Slave Collar from ranged enemy in corner at spewing lava", "Lore" },
        { "Lava Ruins - Chest in spewing lava room", "Chest" },
        { "Lava Ruins - Fire spell chest at double staircase", "Chest" },
        { "Lava Ruins - 31. Slave Tag from pot atop of double staircase", "Lore" },
        { "Lava Ruins - Fire magic switch", "Barrier" },
        { "Lava Ruins - 25. Copper Ingot on path through hole in wall", "Lore" },
        { "Lava Ruins - 22. Intricate Clock from barrel in lava maze", "Lore" },
        { "Lava Ruins - Chest in lava maze", "Chest" },
        { "Lava Ruins - Jumping puzzle arcane chest at moving ring gauntlet", "Chest" },
        { "Lava Ruins - Monica shortcut switch", "Metal Gate" },
        { "Lava Ruins - 21. Silver Coin from barrel at Monica statue", "Lore" },
        { "Lava Ruins - 24. Glass Lantern from scissor enemy", "Lore" },
        { "Lava Ruins - Monica", "Bosses" },
        { "Lava Ruins - 34. Bestian Ear from Monica", "Lore" },
        { "Lava Ruins - 33. Bestian Palm from Monica", "Lore" },
        { "Lava Ruins - 99. Child's Soul Shard from Monica", "Lore" },
        { "Lava Ruins - Monica warp gate switch", "Metal Gate" },
        { "Lava Ruins - Ice spell chest in corner on path to dark tunnel", "Chest" },
        { "Dark Tunnel - 5. Melted Silver Candlestick in front of underground shortcut", "Lore" },
        { "Dark Tunnel - 39. Dark Elf's Short Bow from barrel on scaffolding", "Lore" },
        { "Dark Tunnel - 43. Bloodstained Javelin from barrel at statue", "Lore" },
        { "Dark Tunnel - 40. Ogre's Kidney from first shield enemy", "Lore" },
        { "Dark Tunnel - 41. Ogre's Eye from first ranged enemy", "Lore" },
        { "Dark Tunnel - 44. Nomad's Cookware from barrel at magic barrier switch", "Lore" },
        { "Dark Tunnel - First magic barrier switch", "Barrier" },
        { "Dark Tunnel - 42. Ogre's Club from shield enemy at gate switch", "Lore" },
        { "Dark Tunnel - First gate switch", "Metal Gate" },
        { "Dark Tunnel - Absorption spell chest hidden behind rubble at staircase", "Chest" },
        { "Dark Tunnel - 45. Golden Coin from first mimic", "Lore" },
        { "Dark Tunnel - Chest at jump from broken staircase", "Chest" },
        { "Dark Tunnel - 46. Tooth Thief's Pouch from barrel at scaffolding", "Lore" },
        { "Dark Tunnel - Wind spell chest in dark hole", "Chest" },
        { "Dark Tunnel - 50. Crafted Soul Injector from pot at light switch after getting the hat", "Lore" },
        { "Dark Tunnel - Light switch after getting the hat", "Barrier" },
        { "Dark Tunnel - 57. Lady's Feather Hat from puppeteer", "Lore" },
        { "Dark Tunnel - 47. Blood Orc's Skin Sample from barrel above dark maze", "Lore" },
        { "Dark Tunnel - Fire spell chest inside dark maze", "Chest" },
        { "Dark Tunnel - 48. Chief's Skull from right mimic in mimic room", "Lore" },
        { "Dark Tunnel - 49. Chief's Skull from straight mimic in mimic room", "Lore" },
        { "Dark Tunnel - Thunder spell chest in mimic room", "Chest" },
        { "Dark Tunnel - 53. Ceremonial Sword from barrel at second statue", "Lore" },
        { "Dark Tunnel - Thunder barrier magic switches", "Barrier" },
        { "Dark Tunnel - 51. Hero's Insignia", "Lore" },
        { "Dark Tunnel - Chest in alcove", "Chest" },
        { "Dark Tunnel - 54. Banner of the Lance Hero from lightning enemy", "Lore" },
        { "Dark Tunnel - Floating platform switch one", "Barrier" },
        { "Dark Tunnel - Floating platform switch two", "Barrier" },
        { "Dark Tunnel - Floating platform switch three", "Barrier" },
        { "Dark Tunnel - Chest on left side after floating platforms", "Chest" },
        { "Dark Tunnel - Arcane spell chest on collapsed bridge", "Chest" },
        { "Dark Tunnel - 52. Mutated Beast Claw from pot in big hall", "Lore" },
        { "Dark Tunnel - 69. Pontiff's Scepter from singular barrel in side room", "Lore" },
        { "Dark Tunnel - 71. Apocalypse Knight Record from knight enemy", "Lore" },
        { "Dark Tunnel - 103. Loyal Soul Shard from knight enemy", "Lore" },
        { "Dark Tunnel - Chest after knight enemy", "Chest" },
        { "Dark Tunnel - 65. Declaration of War from barrel at statue", "Lore" },
        { "Dark Tunnel - Vanessa", "Bosses" },
        { "Dark Tunnel - 100. King's Final Honor from Vanessa", "Lore" },
        { "Dark Tunnel - 78. Ancient Throne Rune from Vanessa", "Lore" },
        { "Dark Tunnel - 77. The Throne from Vanessa", "Lore" },
        { "Spirit Realm - 72. Apocalypse Knight Shield from crystal at statue", "Lore" },
        { "Spirit Realm - 73. Apocalypse Knight Axe from enemy in trapped barrel", "Lore" },
        { "Spirit Realm - 79. Doctor's Mask hidden on ledge", "Lore" },
        { "Spirit Realm - Wind spell chest inside roof hole", "Chest" },
        { "Spirit Realm - Wind spell chest gate switch", "Metal Gate" },
        { "Spirit Realm - 55. Banner of the Lionhearted from crystal in doorway", "Lore" },
        { "Spirit Realm - 75. Apocalypse Knight Bow from bow enemy", "Lore" },
        { "Spirit Realm - 61. Knight Kingdom Entry Pass from sword enemy", "Lore" },
        { "Spirit Realm - 58. Super Cleanser Soap from crystal on path", "Lore" },
        { "Spirit Realm - Chest hidden in dropdown alcove", "Chest" },
        { "Spirit Realm - 63. Inquisitor's List from crystal at second statue", "Lore" },
        { "Spirit Realm - Ice spell chest in right side alcove", "Chest" },
        { "Spirit Realm - Ice spell chest gate switch in right side alcove", "Metal Gate" },
        { "Spirit Realm - 76. Apocalypse Knight Sword from enemy", "Lore" },
        { "Spirit Realm - 74. Apocalypse Knight Staff from enemy", "Lore" },
        { "Spirit Realm - Arcane barrier magic switches", "Barrier" },
        { "Spirit Realm - Thunder spell chest behind breakable wall", "Chest" },
        { "Spirit Realm - 86. Missing Person Poster on bridge", "Lore" },
        { "Spirit Realm - Platform shortcut switch", "Barrier" },
        { "Spirit Realm - 59. Exquisite Leather Lamp from crystal before Seal", "Lore" },
        { "Spirit Realm - 66. Attic Key from crystal at first Seal phase", "Lore" },
        { "Spirit Realm - 64. Envoy's Rune from crystal at first Seal phase", "Lore" },
        { "Spirit Realm - 60. Premium Grass Ash from crystal at first Seal phase", "Lore" },
        { "Spirit Realm - 62. Bone Chess Set from crystal at first Seal phase", "Lore" },
        { "Spirit Realm - First Seal magic barrier", "Barrier" },
        { "Spirit Realm - 90. Enchanted Shackles from second Seal phase", "Lore" },
        { "Spirit Realm - Fire spell chest at second Seal phase", "Chest" },
        { "Spirit Realm - Second Seal magic barrier", "Barrier" },
        { "Spirit Realm - 87. Saint's Cane from crystal at third statue", "Lore" },
        { "Spirit Realm - Statue shortcut gate switch", "Metal Gate" },
        { "Spirit Realm - 67. Halfling's Forelimb from crystal at elevator magic switch", "Lore" },
        { "Spirit Realm - Elevator magic switch", "Barrier" },
        { "Spirit Realm - 68. Ratian Claw from crystal after elevator", "Lore" },
        { "Spirit Realm - Absorption spell chest behind crystals after elevator", "Chest" },
        { "Spirit Realm - Fire control magic switch", "Barrier" },
        { "Spirit Realm - Magic switch barrier switch", "Barrier" },
        { "Spirit Realm - Teleporter magic switch", "Barrier" },
        { "Spirit Realm - 70. Abandoned Rag Doll from crystal on spiral stairs", "Lore" },
        { "Spirit Realm - Chest before boss behind breakable wall", "Chest" },
        { "Spirit Realm - 89. Bloodstained Key at statue before boss", "Lore" },
        { "Spirit Realm - Vanessa V2", "Bosses" },
        { "Spirit Realm - 101. Proud King's Crafted Soul Shard from Vanessa V2", "Lore" },
        { "Spirit Realm - Thunder spell from Vanessa V2", "Item" },
        { "Abyss - 80. Ceremonial Incense at first statue", "Lore" },
        { "Abyss - First gate switch", "Metal Gate" },
        { "Abyss - 81. Moonlight Blade from trapped barrel", "Lore" },
        { "Abyss - 83. Castle Blueprint from crystal on brittle ledge", "Lore" },
        { "Abyss - Arcane spell chest on pillar", "Chest" },
        { "Abyss - 84. Witch Worshipper Puppet from pot right of pillars", "Lore" },
        { "Abyss - Giant maid barrier", "Barrier" },
        { "Abyss - 82. Prostitute's Chiffon from crystal in left trap gate", "Lore" },
        { "Abyss - Chest in left trap gate", "Chest" },
        { "Abyss - 85. Polymorphism Scroll from crystal behind gate", "Lore" },
        { "Abyss - 88. Hero Summon Rune in front of statue", "Lore" },
        { "Abyss - Fire Spell chest underground trial", "Chest" },
        { "Abyss - Underground trial unlock enemies magic switch", "Barrier" },
        { "Abyss - 91. Gaseous Soul Essence from scissor enemy in underground trial", "Lore" },
        { "Abyss - Underground trial scissor enemy magic gate", "Barrier" },
        { "Abyss - Underground trial magic switch", "Barrier" },
        { "Abyss - 92. Semi-gaseous Soul Essence in front of underground trial magic switch", "Lore" },
        { "Abyss - Thunder spell chest dark tunnel trial", "Chest" },
        { "Abyss - 95. Refined Soul Shard from maid enemy in dark tunnel trial", "Lore" },
        { "Abyss - Dark tunnel trial maid enemy barrier", "Barrier" },
        { "Abyss - Dark Tunnel trial magic switch", "Barrier" },
        { "Abyss - 96. Knight's Soul Shard in front of dark tunnel trial magic switch", "Lore" },
        { "Abyss - Ice spell chest lava ruins trial", "Chest" },
        { "Abyss - 93. Enchanted Soul Shard from maid enemy in lava ruins trial", "Lore" },
        { "Abyss - Lava Ruins trial defeat maids enemy barrier", "Barrier" },
        { "Abyss - Lava Ruins trial magic switch", "Barrier" },
        { "Abyss - 94. Knight's Soul Shard in front of lava ruins trial magic switch", "Lore" },
        { "Abyss - 102. Lost Maiden's Crafted Soul Shard from Nonota", "Lore" },
        { "Abyss - Nonota", "Event" },
    };

    public static SortedDictionary<string, string> Items = new()
    {
        { "Arcane", "Attack Magics" },
        { "Ice", "Attack Magics" },
        { "Fire", "Attack Magics" },
        { "Thunder", "Attack Magics" },
        { "Wind", "Double Jump" },
        { "Mana Absorption", "Counter" },
        { "Progressive Bag Upgrade", "Bag Upgrade" },
        { "Specter Armor Soul", "Boss Souls" },
        { "Tania Soul", "Boss Souls" },
        { "Monica Soul", "Boss Souls" },
        { "Enraged Armor Soul", "Boss Souls" },
        { "Vanessa Soul", "Boss Souls" },
        { "Vanessa V2 Soul", "Boss Souls" },
        { "HPCure", "Filler" },
        { "HPCureMiddle", "Filler" },
        { "HPCureBig", "Filler" },
        { "MPCure", "Filler" },
        { "MPCureMiddle", "Filler" },
        { "MPCureBig", "Filler" },
        { "Defense", "Filler" },
        { "DefenseMiddle", "Filler" },
        { "DefenseBig", "Filler" },
        { "Souls", "Filler" },
        { "1. Crafted Soul Reader", "Lore" },
        { "2. Stained Ribbon", "Lore" },
        { "3. Copper Coin", "Lore" },
        { "4. Unknown House Banner", "Lore" },
        { "5. Melted Silver Candlestick", "Lore" },
        { "6. Broken Cross Spear", "Lore" },
        { "7. Deformed Cavalier Armor", "Lore" },
        { "8. Hero's Cross Sword", "Lore" },
        { "9. Giant Axe", "Lore" },
        { "10. Shield of the Church", "Lore" },
        { "11. Broken Queen Doll", "Lore" },
        { "12. Pointy Witch's Hat", "Lore" },
        { "13. Sleeve Dagger", "Lore" },
        { "14. Corpse Shroud", "Lore" },
        { "15. Headcutter Circular Saw", "Lore" },
        { "16. Test Subject Manacle", "Lore" },
        { "17. Tattered Maid Outfit", "Lore" },
        { "18. Soul Doll Remnant", "Lore" },
        { "19. Knight's Halberd", "Lore" },
        { "20. Weathered Cloak", "Lore" },
        { "21. Silver Coin", "Lore" },
        { "22. Intricate Clock", "Lore" },
        { "24. Cursed Turquoise Necklace", "Lore" },
        { "24. Glass Lantern", "Lore" },
        { "25. Copper Ingot", "Lore" },
        { "26. Teddy Bear", "Lore" },
        { "27. Fractured Stone Axe", "Lore" },
        { "28. Cage", "Lore" },
        { "29. Slave Branding Iron", "Lore" },
        { "30. Merchant's Ledger", "Lore" },
        { "31. Slave Tag", "Lore" },
        { "32. Slave Collar", "Lore" },
        { "33. Bestian Palm", "Lore" },
        { "34. Bestian Ear", "Lore" },
        { "35. Dwarven Metalwork", "Lore" },
        { "36. High Elf's Mana Ring", "Lore" },
        { "37. Forest Elf's Vest", "Lore" },
        { "38. Dark Elf's Ear Sample", "Lore" },
        { "39. Dark Elf's Short Bow", "Lore" },
        { "40. Ogre's Kidney", "Lore" },
        { "41. Ogre's Eye", "Lore" },
        { "42. Ogre's Club", "Lore" },
        { "43. Bloodstained Javelin", "Lore" },
        { "44. Nomad's Cookware", "Lore" },
        { "45. Golden Coin", "Lore" },
        { "46. Tooth Thief's Pouch", "Lore" },
        { "47. Blood Orc's Skin Sample", "Lore" },
        { "48. Chief's Skull", "Lore" },
        { "49. Chief's Skull", "Lore" },
        { "50. Crafted Soul Injector", "Lore" },
        { "51. Hero's Insignia", "Lore" },
        { "52. Mutated Beast Claw", "Lore" },
        { "53. Ceremonial Sword", "Lore" },
        { "54. Banner of the Lance Hero", "Lore" },
        { "55. Banner of the Lionhearted", "Lore" },
        { "56. Knight Kingdom Crown", "Lore" },
        { "57. Lady's Feather Hat", "Lore" },
        { "58. Super Cleanser Soap", "Lore" },
        { "59. Exquisite Leather Lamp", "Lore" },
        { "60. Premium Grass Ash", "Lore" },
        { "61. Knight Kingdom Entry Pass", "Lore" },
        { "62. Bone Chess Set", "Lore" },
        { "63. Inquisitor's List", "Lore" },
        { "64. Envoy's Rune", "Lore" },
        { "65. Declaration of War", "Lore" },
        { "66. Attic Key", "Lore" },
        { "67. Halfling's Forelimb", "Lore" },
        { "68. Ratian Claw", "Lore" },
        { "69. Pontiff's Scepter", "Lore" },
        { "70. Abandoned Rag Doll", "Lore" },
        { "71. Apocalypse Knight Record", "Lore" },
        { "72. Apocalypse Knight Shield", "Lore" },
        { "73. Apocalypse Knight Axe", "Lore" },
        { "74. Apocalypse Knight Staff", "Lore" },
        { "75. Apocalypse Knight Bow", "Lore" },
        { "76. Apocalypse Knight Sword", "Lore" },
        { "77. The Throne", "Lore" },
        { "78. Ancient Throne Rune", "Lore" },
        { "79. Doctor's Mask", "Lore" },
        { "80. Ceremonial Incense", "Lore" },
        { "81. Moonlight Blade", "Lore" },
        { "82. Prostitute's Chiffon", "Lore" },
        { "83. Castle Blueprint", "Lore" },
        { "84. Witch Worshipper Puppet", "Lore" },
        { "85. Polymorphism Scroll", "Lore" },
        { "86. Missing Person Poster", "Lore" },
        { "87. Saint's Cane", "Lore" },
        { "88. Hero Summon Rune", "Lore" },
        { "89. Bloodstained Key", "Lore" },
        { "90. Enchanted Shackles", "Lore" },
        { "91. Gaseous Soul Essence", "Lore" },
        { "92. Semi-gaseous Soul Essence", "Lore" },
        { "93. Enchanted Soul Shard", "Lore" },
        { "94. Knight's Soul Shard", "Lore" },
        { "95. Refined Soul Shard", "Lore" },
        { "96. Knight's Soul Shard", "Lore" },
        { "97. Faithful Soul Shard", "Lore" },
        { "98. Lost Maiden's Soul Shard", "Lore" },
        { "99. Child's Soul Shard", "Lore" },
        { "100. King's Final Honor", "Lore" },
        { "101. Proud King's Crafted Soul Shard", "Lore" },
        { "102. Lost Maiden's Crafted Soul Shard", "Lore" },
        { "103. Loyal Soul Shard", "Lore" },
        { "Shrine First Magic Barrier", "Magic Barrier" },
        { "Shrine Second Magic Barrier", "Magic Barrier" },
        { "Shrine Meet Cat Magic Barrier", "Magic Barrier" },
        { "Secret Passage Entrance Magic Barrier", "Magic Barrier" },
        { "Secret Passage First Fire Barrier", "Magic Barrier" },
        { "Secret Passage Second Fire Barrier", "Magic Barrier" },
        { "Shrine Secret Area Shortcut Gate", "Magic Barrier" },
        { "Defeat Enraged Armor Barrier", "Magic Barrier" },
        { "Underground Magic Barrier At Maid Enemy", "Magic Barrier" },
        { "Underground Fire Barrier Magic Barrier", "Magic Barrier" },
        { "Underground Enemy Magic Barrier", "Magic Barrier" },
        { "Lava Ruins Magic Platforms", "Magic Barrier" },
        { "Lava Ruins Scissor Enemy Barrier", "Magic Barrier" },
        { "Lava Ruins Scissor Enemy Lift", "Magic Barrier" },
        { "Lava Ruins Fire Magic Barrier", "Magic Barrier" },
        { "Dark Tunnel First Magic Barrier", "Magic Barrier" },
        { "Dark Tunnel Light Switch Barrier", "Magic Barrier" },
        { "Dark Tunnel Thunder Barrier", "Magic Barrier" },
        { "Dark Tunnel Floating Platform One", "Magic Barrier" },
        { "Dark Tunnel Floating Platform Two", "Magic Barrier" },
        { "Dark Tunnel Floating Platform Three", "Magic Barrier" },
        { "Spirit Realm Arcane Barrier", "Magic Barrier" },
        { "Spirit Realm First Seal Magic Barrier", "Magic Barrier" },
        { "Spirit Realm Second Seal Magic Barrier", "Magic Barrier" },
        { "Spirit Realm Elevator", "Magic Barrier" },
        { "Spirit Realm Teleporter", "Magic Barrier" },
        { "Abyss After Giant Maid Barrier", "Magic Barrier" },
        { "Abyss Underground Trial Magic Switch", "Magic Barrier" },
        { "Abyss After Scissor Enemy Barrier", "Magic Barrier" },
        { "Abyss Lava Ruins Trial Magic Switch", "Magic Barrier" },
        { "Abyss Dark Tunnel Trial Magic Switch", "Magic Barrier" },
        { "Abyss Dark Tunnel Trial Maid Enemy Barrier", "Magic Barrier" },
        { "Abyss Lava Ruins Trial Maid Enemy Barrier", "Magic Barrier" },
        { "Spirit Realm Fire Deactivation", "Magic Barrier" },
        { "Spirit Realm Magic Switch Barrier", "Magic Barrier" },
        { "Shrine Secret Boss Shortcut Gate", "Metal Gate" },
        { "Shrine Underground Shortcut Gate", "Metal Gate" },
        { "Secret Passage Dark Tunnel Shortcut Gate", "Metal Gate" },
        { "Underground Lava Ruins Shortcut Gate", "Metal Gate" },
        { "Underground Tania Shortcut Gate On Grand Hall Side", "Metal Gate" },
        { "Underground Tania Shortcut Gate On Tania Side", "Metal Gate" },
        { "Lava Ruins Monica Warp Gate", "Metal Gate" },
        { "Dark Tunnel First Gate", "Metal Gate" },
        { "Spirit Realm Statue Shortcut Gate", "Metal Gate" },
        { "Abyss First Gate", "Metal Gate" },
        { "Abyss Left Trap Gate", "Metal Gate" },
        { "Spirit Realm - Wind Spell Chest Gate", "Metal Gate" },
        { "Lava Ruins Fake Floor Shortcut Gate", "Metal Gate" },
        { "Spirit Realm - Ice spell Chest Gate", "Metal Gate" },
        { "Lava Ruins Monica Shortcut Gate", "Metal Gate" },
        { "Teleport", "Teleport" },
        { "Trial Key", "Trial Key" },
    };

    public static string GameLocationToDescriptiveLocation (string gameLocation)
    {
        string descriptiveLocation = "";
        switch (gameLocation)
        {
            // shrine_start_locations
            case "TreasureBox_Room03":
                descriptiveLocation = "Shrine - Chest at first magic switch barrier";
                break;

            case "TreasureBox02_Room03":
                descriptiveLocation = "Shrine - Chest on platform at first magic switch barrier";
                break;
            // shrine_armor_locations
            case "TreasureBox_Room05":
                descriptiveLocation = "Shrine - Chest behind gate";
                break;

            case "Boss_Act01":
                descriptiveLocation = "Shrine - Specter Armor";
                break;
            // shrine_secret_passage_locations
            case "TreasureBox07":
                descriptiveLocation = "Secret Passage - Absorption spell chest behind destructible wall";
                break;

            case "TreasureBox07To08":
                descriptiveLocation = "Secret Passage - Wind spell chest in alcove during fall";
                break;

            case "TreasureBox08":
                descriptiveLocation = "Secret Passage - Ice spell chest behind breakable wall after fall";
                break;

            case "TreasureBox09":
                descriptiveLocation = "Secret Passage - Fire spell chest at second fire barrier magic switch";
                break;

            case "TreasureBox10":
                descriptiveLocation = "Secret Passage - Thunder spell chest after boss";
                break;

            case "Boss_Act01_Plus":
                descriptiveLocation = "Secret Passage - Enraged Armor";
                break;
            // underground_start_locations
            case "TreasureBox_Room01":
                descriptiveLocation = "Underground - Chest Room01";
                break;
            // Handled elsewhere because name appears twice
            /*case "TreasureBox_Room03":
                descriptiveLocation = "Underground - Chest Room03";
                break;
            */
            case "TreasureBox_Room04":
                descriptiveLocation = "Underground - Chest Room04";
                break;

            case "TreasureBox_Room05_01":
                descriptiveLocation = "Underground - Chest Room05_01";
                break;

            case "TreasureBox_Room05_02":
                descriptiveLocation = "Underground - Chest Room05_02";
                break;

            case "Cat":
                descriptiveLocation = "Underground - Cat";
                break;
            //underground_tania_locations
            case "TreasureBox_Room08":
                descriptiveLocation = "Underground - Chest Room08";
                break;

            case "Boss_Level02":
                descriptiveLocation = "Underground - Tania";
                break;
            // lava_ruins_start_locations
            case "Room02_TreasureBox01":
                descriptiveLocation = "Lava Ruins - Chest Room02_01";
                break;

            case "Room02_TreasureBox02":
                descriptiveLocation = "Lava Ruins - Chest Room02_02";
                break;

            case "Room03_TreasureBox01":
                descriptiveLocation = "Lava Ruins - Chest Room03_01";
                break;

            case "Room03_TreasureBox02":
                descriptiveLocation = "Lava Ruins - Chest Room03_02";
                break;

            case "Room02To04_TreasureBox02":
                descriptiveLocation = "Lava Ruins - Chest Room02To04";
                break;

            case "Room05To06_TreasureBox":
                descriptiveLocation = "Lava Ruins - Chest Room05To06";
                break;

            case "Room06_TreasureBox":
                descriptiveLocation = "Lava Ruins - Chest Room06";
                break;
            // lava_ruins_after_fire_barrier_locations
            case "Room07_TreasureBox":
                descriptiveLocation = "Lava Ruins - Chest Room07";
                break;

            case "Room08_TreasureBox":
                descriptiveLocation = "Lava Ruins - Chest Room08";
                break;

            case "Boss_Level03_Big":
                descriptiveLocation = "Lava Ruins - Monica";
                break;

            case "Room01_TreasureBox":
                descriptiveLocation = "Lava Ruins - Chest Room01";
                break;
            // dark_tunnel_start_locations
            case "TreasureBox02_Room02_03":
                descriptiveLocation = "Dark Tunnel - Chest Room02";
                break;

            case "TreasureBox02_Room03_01":
                descriptiveLocation = "Dark Tunnel - Chest Room03_01";
                break;

            case "TreasureBox02_Room03_02":
                descriptiveLocation = "Dark Tunnel - Chest Room03_02";
                break;

            case "TreasureBox02_Room04":
                descriptiveLocation = "Dark Tunnel - Chest Room04";
                break;

            case "TreasureBox02_Room05":
                descriptiveLocation = "Dark Tunnel - Chest Room05";
                break;
            // dark_tunnel_after_thunder_locations
            case "TreasureBox02_Room06To07":
                descriptiveLocation = "Dark Tunnel - Chest Room06To07";
                break;

            case "TreasureBox02_Room07":
                descriptiveLocation = "Dark Tunnel - Chest Room07";
                break;

            case "TreasureBox02_Room08":
                descriptiveLocation = "Dark Tunnel - Chest Room08";
                break;

            case "TreasureBox02_Room09To10":
                descriptiveLocation = "Dark Tunnel - Chest Room09To10";
                break;

            case "Boss_Level04":
                descriptiveLocation = "Dark Tunnel - Vanessa";
                break;
            // spirit_realm_start_locations
            case "TreasureBox02_R02":
                descriptiveLocation = "Spirit Realm - Chest Room02";
                break;

            case "TreasureBox02_R03":
                descriptiveLocation = "Spirit Realm - Chest Room03";
                break;

            case "TreasureBox02_R0401":
                descriptiveLocation = "Spirit Realm - Chest Room04_01";
                break;
            // spirit_realm_after_arcane_barrier_locations
            case "TreasureBox02_R0402":
                descriptiveLocation = "Spirit Realm - Chest Room04_02";
                break;

            case "TreasureBox02_R06":
                descriptiveLocation = "Spirit Realm - Chest Room06";
                break;

            case "TreasureBox02_R07":
                descriptiveLocation = "Spirit Realm - Chest Room07";
                break;
            // spirit_realm_after_teleport_locations
            case "TreasureBox02_R08":
                descriptiveLocation = "Spirit Realm - Chest Room08";
                break;

            case "Boss_Level05":
                descriptiveLocation = "Spirit Realm - Queen Vanessa V2";
                break;
            // abyss_locations
            case "TreasureBox_Act02Room04":
                descriptiveLocation = "Abyss - Chest Room04";
                break;

            case "TreasureBox_Act02Room05":
                descriptiveLocation = "Abyss - Chest Room05";
                break;
            // abyss_trials_locations
            case "Act04Room05To06_TreasureBox":
                descriptiveLocation = "Abyss - Chest Lava Ruins Trial";
                break;

            case "Act05_TreasureBox02_Room09To10":
                descriptiveLocation = "Abyss - Chest Dark Tunnel Trial";
                break;

            case "Act03TreasureBox_Room05_02":
                descriptiveLocation = "Abyss - Chest Underground Trial";
                break;

            case "Nonota":
                descriptiveLocation = "Abyss - Nonota";
                break;
        }

        return descriptiveLocation;
    }

    public static Dictionary<string, ItemSystem.ItemType> DescriptiveItemToGameItemMap = new()
    {
        {"Arcane", ItemSystem.ItemType.MagicNull },
        {"Ice", ItemSystem.ItemType.MagicIce },
        {"Fire", ItemSystem.ItemType.MagicFire },
        {"Thunder", ItemSystem.ItemType.MagicLightning },
        {"Wind", ItemSystem.ItemType.SkyJump },
        {"Mana Absorption", ItemSystem.ItemType.Absorb },
        {"Progressive Bag Upgrade", ItemSystem.ItemType.BagMaxAdd },
        {"Souls", ItemSystem.ItemType.Null }
    };

    public static long GetLocationIdByName (string name) => Locations.Keys.ToList().IndexOf(name);

    public static long GetItemIdByName (string name) => Items.Keys.ToList().IndexOf(name);
}