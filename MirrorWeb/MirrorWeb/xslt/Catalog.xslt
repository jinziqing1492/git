<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <xsl:if test="toc/tocdiv[node()!='']">
      <xsl:apply-templates select="toc/tocdiv"/>
    </xsl:if>
  </xsl:template>
  <xsl:template match="toc/tocdiv">
    <p>
      <a href="javascript:void(0);">
        <xsl:value-of select="./title"/>
      </a>
      <span style="display:none;">
        <xsl:value-of select="@pagenum"/>
      </span>
    </p>
    <xsl:for-each select="tocdiv">
      <span style="display: block; padding-left:2em;">
        <a href="javascript:void(0);">
          <xsl:value-of select="./title"/>
        </a>
        <span style="display:none;">
          <xsl:value-of select="@pagenum"/>
        </span>
        <xsl:if test="tocdiv[node()!='']" >
          <xsl:for-each select="tocdiv">
            <span style="display: block; padding-left:2em;">
              <a href="javascript:void(0);">
                <xsl:value-of select="./title"/>
              </a>
              <span style="display:none;">
                <xsl:value-of select="@pagenum"/>
              </span>
              <xsl:if test="tocdiv[node()!='']" >
                <xsl:for-each select="tocdiv">
                  <span style="display: block; padding-left:2em;">
                    <a href="javascript:void(0);">
                      <xsl:value-of select="./title"/>
                    </a>
                    <span style="display:none;">
                      <xsl:value-of select="@pagenum"/>
                    </span>
                  </span>
                </xsl:for-each>
              </xsl:if>

            </span>
          </xsl:for-each>
        </xsl:if>
      </span>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>