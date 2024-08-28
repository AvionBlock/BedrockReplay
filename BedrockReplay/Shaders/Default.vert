#version 330 core
layout (location = 0) in vec3 aPosition; //vertex coordinates

//uniform variables
uniform mat4 MVP;

void main() {
	gl_Position = vec4(aPosition, 1.0) * MVP; //Coordinates
}