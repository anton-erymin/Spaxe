// Extractor.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Extractor.h"


OggVorbis_File vf;


VOID display_info()
{
	vorbis_info* info = ov_info(&vf, -1);
	vorbis_comment* comment = ov_comment(&vf, -1);
	ogg_int64_t total = ov_pcm_total(&vf, -1);

	printf("Bitstream is %d channel, %ldHz\n", info->channels, info->rate);
	printf("Decoded length: %ld samples\n", total);
	printf("Encoded by: %s\n", comment->vendor);
}


VOID write_header(FILE* f, ogg_int64_t total)
{
	PCMWAVEFORMAT format;

	format.wf.wFormatTag		= 1;
	format.wf.nChannels			= 1;
	format.wf.nSamplesPerSec	= 44100;
	format.wf.nAvgBytesPerSec	= 88200;
	format.wf.nBlockAlign		= 2;
	format.wBitsPerSample		= 16;

	char riff[] = "RIFF";
	char wave[] = "WAVEfmt ";
	char data[] = "data";

	DWORD size;

	size = sizeof(wave) - 1 + sizeof(size) + sizeof(format) + sizeof(data) - 1 + sizeof(size) + total;
	fwrite(&riff, sizeof(riff) - 1, 1, f);
	fwrite(&size, sizeof(size), 1, f);

	size = sizeof(format);
	fwrite(&wave, sizeof(wave) - 1, 1, f);
	fwrite(&size, sizeof(size), 1, f);
	fwrite(&format, sizeof(format), 1, f);
	
	size = total;
	fwrite(&data, sizeof(data) - 1, 1, f);
	fwrite(&size, sizeof(size), 1, f);
}


VOID write_sound(FILE* f, ogg_int64_t position, ogg_int64_t length)
{
	ov_pcm_seek(&vf, position);

	char buffer[4096];
	int current = 0;
	ogg_int64_t summary = 0;

	
	while (true)
	{
		long result = ov_read(&vf, buffer, sizeof(buffer), 0, 2, 1, &current);
		if (result == 0) break;

		if (summary + result >= 2 * length)
		{
			fwrite(buffer, 2 * length - summary, 1, f);
			break;
		}
		else
		{
			fwrite(buffer, result, 1, f);
			summary += result;
		}
	}
}


VOID write_silence(FILE* f, ogg_int64_t length)
{
	char buffer[2] = {0, 0};

	for (int i = 0; i < length; i++)
	{
		fwrite(buffer, sizeof(buffer), 1, f);
	}
}


EXTRACTOR_API int Process(HANDLE handle, INT pause, UINT data[], INT count, ULONG total)
{
	int descriptor = _open_osfhandle((LONG)handle, _O_RDONLY);
	FILE* file = fdopen(descriptor, "rb");

	
	ov_open(file, &vf, NULL, 0);

	display_info();


	if (ov_seekable(&vf))
	{
		FILE* f = fopen("Output.wav", "wb");

		ogg_int64_t interval = pause * 44100;

		write_header(f, 2 * (total + count * interval));

		for (int i = 0; i < count - 1; i = i + 2)
		{
			ogg_int64_t position	= data[i];
			ogg_int64_t length		= data[i + 1];

			write_sound(f, position, length);
			write_silence(f, interval);
		}

		fclose(f);
	}


	ov_clear(&vf);

	printf("Extraction complete\n");

	return 0;
}
