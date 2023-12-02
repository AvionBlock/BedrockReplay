#version 330 core

in vec2 texCoord;

out vec4 FragColor;

uniform sampler2D texture0;

void main() {
	vec4 texel = texture(texture0, texCoord);
	if(texel.a < 0.5)
		discard;

	FragColor = texel;
}