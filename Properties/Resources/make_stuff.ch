using minecraft;

namespace mydatapack {

	[minecraft:tick]
	function make_stuff {
		/*
			A function to kill all bats as soon as they spawn
		*/
		as @e[type = bat
			// ,tag =! dying
			] {
			say("I was a Bat");
			// tag @s dying;
			// kill @s;
		}
	}
}