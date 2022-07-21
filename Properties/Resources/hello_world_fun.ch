using minecraft;

namespace mydatapack {

	[minecraft:load]
	/*
		Multiline Comment
	*/
	function hello_world_fun {
		// Comment
		as @e[type = player]
			at @s {
				say("hello world");
				say("This is a SeaHopper test");
			}
	}
}
