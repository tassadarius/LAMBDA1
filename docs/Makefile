# IMAGES_PATH=$(wilrdcard images/*.tex)
IMAGES_PATH=tikz

all: overview round lfsr clean

overview: $(IMAGES_PATH)/lambda1_overview.tex
	cd $(IMAGES_PATH); \
	latex --shell-escape lambda1_overview.tex; \
	cp lambda1_overview.png ../images

round: $(IMAGES_PATH)/round_overview.tex
	cd $(IMAGES_PATH); \
	latex --shell-escape round_overview.tex; \
	cp round_overview.png  ../images

lfsr: $(IMAGES_PATH)/lfsr.tex
	cd $(IMAGES_PATH); \
	latex --shell-escape lfsr.tex; \
	cp lfsr.png ../images

.PHONY clean:
clean:
	rm $(IMAGES_PATH)/*.png $(IMAGES_PATH)/*.log $(IMAGES_PATH)/*.ps $(IMAGES_PATH)/*.dvi $(IMAGES_PATH)/*.aux
