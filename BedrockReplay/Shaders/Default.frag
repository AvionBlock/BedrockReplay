#version 330 core
out vec4 out_color;
in vec3 ourColor;

void main() {
	out_color = vec4(ourColor,1.0);
}