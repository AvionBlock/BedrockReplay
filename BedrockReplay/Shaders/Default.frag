#version 330 core
out vec4 out_color;

in vec3 outColor;

void main() {
	out_color = vec4(outColor + vec3(0.5,0.5,0.5), 1.0);
}